using Gen3_Starter_Manip_for_ACE;

namespace Gen3_Starter_Manip_for_ACE.Tests
{
    public class ConsecutiveFilterTests
    {
        [Fact]
        public void 連続数が足りない結果は全部落とされる()
        {
            var results = Fake.Results(10, 20, 30);

            var filtered = ConsecutiveFilter.Filter(results, minCount: 2);

            Assert.Empty(filtered);
        }

        [Fact]
        public void 途中にある連続区間だけが残る()
        {
            //                        ↓ 20,21,22 が3連続
            var results = Fake.Results(10, 20, 21, 22, 40);

            var filtered = ConsecutiveFilter.Filter(results, minCount: 3);

            Assert.Equal([20, 21, 22], Fake.Frames(filtered));
        }

        [Fact]
        public void 末尾で終わる連続区間も正しい範囲で残る()
        {
            //                            ↓ 末尾 30,31,32 が3連続
            var results = Fake.Results(10, 30, 31, 32);

            var filtered = ConsecutiveFilter.Filter(results, minCount: 3);

            Assert.Equal([30, 31, 32], Fake.Frames(filtered));
        }

        [Fact]
        public void 全件が連続している場合は全件残る()
        {
            var results = Fake.Results(1, 2, 3, 4, 5);

            var filtered = ConsecutiveFilter.Filter(results, minCount: 5);

            Assert.Equal([1, 2, 3, 4, 5], Fake.Frames(filtered));
        }

        [Fact]
        public void 連続区間が複数あれば全部残る()
        {
            var results = Fake.Results(1, 2, 3, 10, 20, 21, 22);

            var filtered = ConsecutiveFilter.Filter(results, minCount: 3);

            Assert.Equal([1, 2, 3, 20, 21, 22], Fake.Frames(filtered));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void 要素が1件以下でも例外にならない(int count)
        {
            var results = Fake.Results(Enumerable.Range(1, count).ToArray());

            var filtered = ConsecutiveFilter.Filter(results, minCount: 1);

            Assert.Equal(count, filtered.Count);
        }
    }
}
