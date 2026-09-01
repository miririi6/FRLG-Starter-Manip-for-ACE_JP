using System;

namespace Gen3_Starter_Manip_for_ACE
{
    public readonly struct StepCountResult
    {
        /// <summary>目標経験値に届くまでに回す周回数。</summary>
        public int CycleCount { get; init; }
        /// <summary>周回では吸収しきれない余りの歩数。</summary>
        public int RemainingSteps { get; init; }
    }

    /// <summary>
    /// StepCountHelper.SetStepCounts から表示処理を除いた計算部分。
    /// </summary>
    public static class StepCountCalculator
    {
        public static StepCountResult Calculate(
            int targetExp, int initialExp, int fixedStepCount, int offset, int stepsPerCycle)
        {
            if (stepsPerCycle == 0) stepsPerCycle = 1; // 0除算防止（現行 UI と同じ扱い）

            int adjustedRequiredSteps = targetExp - initialExp - (fixedStepCount + offset);

            return new StepCountResult
            {
                CycleCount = (int)Math.Floor((double)adjustedRequiredSteps / stepsPerCycle),
                RemainingSteps = adjustedRequiredSteps % stepsPerCycle,
            };
        }
    }
}
