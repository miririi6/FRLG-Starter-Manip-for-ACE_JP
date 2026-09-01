using Gen3_Starter_Manip_for_ACE;
using static Gen3_Starter_Manip_for_ACE.Types;

namespace Gen3_Starter_Manip_for_ACE.Tests
{
    /// <summary>
    /// 乱数生成そのもの（GetIVs / GetPID）のテスト。
    /// </summary>
    public class LcgTests
    {
        // ── 現状の実装が返す値をそのまま固定した「特性化テスト」。
        //    まだ「正しい値」ではなく「今の値」なので、実機の結果や他の乱数調整ツールと
        //    突き合わせて検証できたらこのコメントを消す。
        //    それまでの間も、リファクタで挙動が変わったことは検出できる。
        [Theory]
        [InlineData(0x00003039u, 0xD3DC0000u)]
        [InlineData(0x00000000u, 0x00000000u)]
        [InlineData(0xDEADBEEFu, 0x1C01DEADu)]
        public void GetPID_既知のseedに対して同じ性格値を返す(uint seed, uint expected)
        {
            Assert.Equal(expected, SearchEngine.GetPID(seed));
        }

        [Theory]
        [InlineData(0x00003039u, 6, 5, 15, 14, 0, 18)]
        [InlineData(0x00000000u, 30, 11, 26, 19, 20, 17)]
        [InlineData(0xDEADBEEFu, 27, 6, 13, 17, 5, 19)]
        public void GetIVs_既知のseedに対して同じ個体値を返す(
            uint seed, int h, int a, int b, int c, int d, int s)
        {
            var iv = SearchEngine.GetIVs(seed);

            Assert.Equal((h, a, b, c, d, s), (iv.H, iv.A, iv.B, iv.C, iv.D, iv.S));
        }

        // ── こちらは「仕様」のテスト。正解の値を知らなくても書ける。
        [Fact]
        public void GetIVs_個体値は常に0から31の範囲に収まる()
        {
            uint seed = 0x1234_5678;
            for (int i = 0; i < 10000; i++)
            {
                var iv = SearchEngine.GetIVs(seed);
                foreach (int v in new[] { iv.H, iv.A, iv.B, iv.C, iv.D, iv.S })
                {
                    Assert.InRange(v, 0, 31);
                }
                seed = seed * Constants.MUL + Constants.ADD;
            }
        }

        [Fact]
        public void GetPID_同じseedからは常に同じ結果が出る()
        {
            Assert.Equal(SearchEngine.GetPID(0xCAFEBABE), SearchEngine.GetPID(0xCAFEBABE));
        }
    }

    /// <summary>
    /// 検索の絞り込みルールのテスト。
    /// 設定はテストごとにその場で作って渡すので、テスト間で状態が漏れない。
    /// </summary>
    public class SearchPokemonsTests
    {
        const ushort Tid = 31121;

        static ConfigData FrameRange(int minFrame, int maxFrame, Action<ConfigData>? extra = null) =>
            TestConfig.New(c =>
            {
                c.minFrame = minFrame;
                c.maxFrame = maxFrame;
                c.checkedNatures = Enum.GetValues<NatureType>().ToList(); // 既定で全性格を許可
                extra?.Invoke(c);
            });

        [Fact]
        public void 絞り込みが無ければフレーム範囲の件数がそのまま返る()
        {
            var config = FrameRange(100, 200);

            var results = SearchEngine.SearchPokemons(config, Tid);

            Assert.Equal(100, results.Count);
        }

        [Fact]
        public void 結果のフレーム番号は指定範囲の中に収まる()
        {
            var config = FrameRange(100, 200);

            var results = SearchEngine.SearchPokemons(config, Tid);

            Assert.Equal(100, results.First().フレーム);
            Assert.Equal(199, results.Last().フレーム);
        }

        [Fact]
        public void チェックした性格以外は除外される()
        {
            var config = FrameRange(1, 5000, c => c.checkedNatures = [NatureType.Modest]);

            var results = SearchEngine.SearchPokemons(config, Tid);

            Assert.NotEmpty(results); // 「0件だから全部通った」を防ぐ
            Assert.All(results, r => Assert.Equal(NatureType.Modest, r.性格));
        }

        [Fact]
        public void 要求個体値を下回るものは除外される()
        {
            var config = FrameRange(1, 20000, c => c.requiredHIV = 31);

            var results = SearchEngine.SearchPokemons(config, Tid);

            Assert.NotEmpty(results);
            Assert.All(results, r => Assert.Equal(31, r.H));
        }

        [Fact]
        public void 上昇補正の性格には要求個体値の3つ目が使われる()
        {
            // いじっぱり = A上昇 / C下降。requiredAIV は [下降時, 無補正時, 上昇時]。
            var config = FrameRange(1, 20000, c =>
            {
                c.checkedNatures = [NatureType.Adamant];
                c.requiredAIV = [0, 0, 31];
            });

            var results = SearchEngine.SearchPokemons(config, Tid);

            Assert.NotEmpty(results);
            Assert.All(results, r => Assert.Equal(31, r.A));
        }

        [Fact]
        public void 上昇補正の性格には下降時と無補正時の要求個体値が使われない()
        {
            var config = FrameRange(1, 20000, c =>
            {
                c.checkedNatures = [NatureType.Adamant];
                c.requiredAIV = [31, 31, 0]; // 上昇時だけ 0
            });

            var results = SearchEngine.SearchPokemons(config, Tid);

            Assert.NotEmpty(results);
            Assert.Contains(results, r => r.A < 31); // 3つ目(=0)が使われている証拠
        }

        [Fact]
        public void 単発検索と周辺検索は同じフレームに同じ性格値を返す()
        {
            var config = FrameRange(1, 500);
            var whole = SearchEngine.SearchPokemons(config, Tid)
                .ToDictionary(r => r.フレーム, r => r.性格値);

            var around = SearchEngine.SerchAroundFrames(config, Tid, currentFrame: 200);

            Assert.NotEmpty(around);
            Assert.All(around, r =>
            {
                if (whole.TryGetValue(r.フレーム, out uint expected))
                {
                    Assert.Equal(expected, r.性格値);
                }
            });
        }
    }

    /// <summary>
    /// ACE モードの検索。Resources を差し込むので直列実行する。
    /// </summary>
    [Collection(ResourcesCollection.Name)]
    public class SearchPokemonsForAceTests
    {
        [Fact]
        public void 生成できる化けポケモンが無ければ結果は0件になる()
        {
            FakeResources.Clear(); // CSV を読まずに空リソースで動かす
            var config = TestConfig.New(c =>
            {
                c.minFrame = 1;
                c.maxFrame = 5000;
                c.checkedNatures = Enum.GetValues<NatureType>().ToList();
                c.isSearchForACE = true;
            });

            var results = SearchEngine.SearchPokemons(config, 31121);

            Assert.Empty(results);
        }
    }

    /// <summary>
    /// メールワードと経験値の対応表のテスト。Resources をテストから差し込む。
    /// </summary>
    [Collection(ResourcesCollection.Name)]
    public class GetWordExpDataTests
    {
        [Fact]
        public void 経験値を仕込めないサブ乱数なら空を返す()
        {
            FakeResources.Clear();

            // pid % 24 == 0 は Constants.expIn2ndIndex に含まれない
            var actual = SearchEngine.GetWordExpData(new ConfigData(), tid: 0, pid: 0);

            Assert.Empty(actual);
        }

        [Fact]
        public void 一致するワードの組み合わせを経験値付きで返す()
        {
            // key = (ushort)pid ^ tid = 6
            // word.ID = 0x1000 なので key ^ ID = 0x1006 → ID 0x1006 の化けポケモンと一致する
            // checksumDiff = 0x1006 - (Bulbasaur(0) + 1) = 0x1005
            // adj = 0x1005 + 0x1006 = 8203
            FakeResources.Install(
                words: [(0x1000, "ワードA")],
                pokeWords: [],
                corrupted: [(0x1006, "パターンX")]);
            var config = TestConfig.New(c =>
            {
                c.seedPokemon = SeedPokemonType.Bulbasaur;
                c.minExp = 0;
                c.minExp2 = 0;
                c.maxExp = 65536;
            });

            var actual = SearchEngine.GetWordExpData(config, tid: 0, pid: 6);

            var row = Assert.Single(actual);
            Assert.Equal(8203, row.経験値);
            Assert.Equal("ワードA", row.ワード3);
            Assert.Equal("ワードA", row.ワード5);
            Assert.Equal("パターンX", row.パターン);
        }

        [Fact]
        public void 上限経験値を超える組み合わせは返さない()
        {
            FakeResources.Install(
                words: [(0x1000, "ワードA")],
                pokeWords: [],
                corrupted: [(0x1006, "パターンX")]);
            var config = TestConfig.New(c =>
            {
                c.seedPokemon = SeedPokemonType.Bulbasaur;
                c.minExp = 0;
                c.minExp2 = 0;
                c.maxExp = 8202; // 8203 の 1 手前
            });

            var actual = SearchEngine.GetWordExpData(config, tid: 0, pid: 6);

            Assert.Empty(actual);
        }
    }
}
