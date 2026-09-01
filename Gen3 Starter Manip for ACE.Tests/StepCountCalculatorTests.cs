using Gen3_Starter_Manip_for_ACE;

namespace Gen3_Starter_Manip_for_ACE.Tests
{
    public class StepCountCalculatorTests
    {
        [Fact]
        public void 周回数と余り歩数を切り分ける()
        {
            // 必要歩数 1000 - 100 = 900、固定 40 + オフセット 10 を引いて 850
            // 850 / 256 = 3 周 と 82 歩
            var actual = StepCountCalculator.Calculate(
                targetExp: 1000, initialExp: 100, fixedStepCount: 40, offset: 10, stepsPerCycle: 256);

            Assert.Equal(3, actual.CycleCount);
            Assert.Equal(82, actual.RemainingSteps);
        }

        [Fact]
        public void 割り切れる場合は余りが0になる()
        {
            var actual = StepCountCalculator.Calculate(
                targetExp: 612, initialExp: 100, fixedStepCount: 0, offset: 0, stepsPerCycle: 256);

            Assert.Equal(2, actual.CycleCount);
            Assert.Equal(0, actual.RemainingSteps);
        }

        [Fact]
        public void 周回歩数0でも例外にならない()
        {
            // UI の 0除算防止を仕様として固定する
            var actual = StepCountCalculator.Calculate(
                targetExp: 150, initialExp: 100, fixedStepCount: 0, offset: 0, stepsPerCycle: 0);

            Assert.Equal(50, actual.CycleCount);
            Assert.Equal(0, actual.RemainingSteps);
        }

        [Fact]
        public void 目標が現在値を下回ると周回数と余りの符号が食い違う_現状の挙動()
        {
            // 目標 90、現在 100 → 必要歩数 -10。
            // CycleCount は Math.Floor で -1、RemainingSteps は C# の % で -10。
            // -1 周 * 256 + (-10) = -266 で、本来の -10 と一致しない。
            // 現時点では UI 上ありえない入力なので「現状こうなる」ことだけ固定しておく。
            // 仕様を決めたらこのテストを書き換える。
            var actual = StepCountCalculator.Calculate(
                targetExp: 90, initialExp: 100, fixedStepCount: 0, offset: 0, stepsPerCycle: 256);

            Assert.Equal(-1, actual.CycleCount);
            Assert.Equal(-10, actual.RemainingSteps);
        }
    }
}
