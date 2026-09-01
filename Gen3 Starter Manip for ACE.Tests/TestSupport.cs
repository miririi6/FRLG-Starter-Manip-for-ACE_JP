using Gen3_Starter_Manip_for_ACE;

namespace Gen3_Starter_Manip_for_ACE.Tests
{
    /// <summary>
    /// テスト用の設定を組み立てる。
    /// SearchEngine が ConfigData を引数で受け取るようになったので、
    /// 各テストは自分専用の設定を作って渡すだけでよく、
    /// static な Instance を差し替える必要がなくなった。
    /// </summary>
    public static class TestConfig
    {
        public static ConfigData New(Action<ConfigData> configure)
        {
            var config = new ConfigData();
            configure(config);
            return config;
        }
    }

    /// <summary>
    /// Resources.* だけは今も public static 配列なので、差し込むと他のテストに漏れる。
    /// これを触るテストクラスは、この Collection に入れて直列実行させる。
    /// （Resources も引数で渡すようにすれば、この制約は消える）
    /// </summary>
    [CollectionDefinition(ResourcesCollection.Name, DisableParallelization = true)]
    public class ResourcesCollection
    {
        public const string Name = "Resources";
    }

    /// <summary>
    /// Resources.* は CSV を読まないと null のまま。実ファイルに触らず直接差し込む。
    /// </summary>
    public static class FakeResources
    {
        public static void Install(
            (ushort id, string text)[] words,
            (ushort id, string text)[] pokeWords,
            (ushort id, string pattern)[] corrupted)
        {
            Resources.wordData = words
                .Select(w => new Types.WordDataType { ID = w.id, Group = "test", Text = w.text })
                .ToArray();
            Resources.pokeWordData = pokeWords
                .Select(w => new Types.WordDataType { ID = w.id, Group = "test", Text = w.text })
                .ToArray();
            Resources.corruptedPokemonData = corrupted
                .Select(c => new Types.CorruptedPokemon { ID = c.id, Pattern = c.pattern })
                .ToArray();
        }

        public static void Clear() => Install([], [], []);
    }

    /// <summary>検索結果のダミー生成（フレーム番号だけ意味を持たせる）。</summary>
    public static class Fake
    {
        public static List<Types.FilterdStarterPokemonType> Results(params int[] frames) =>
            frames.Select(f => new Types.FilterdStarterPokemonType { フレーム = f }).ToList();

        public static int[] Frames(IEnumerable<Types.FilterdStarterPokemonType> results) =>
            results.Select(r => r.フレーム).ToArray();
    }
}
