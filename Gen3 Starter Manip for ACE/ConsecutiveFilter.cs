using System;
using System.Collections.Generic;

namespace Gen3_Starter_Manip_for_ACE
{
    /// <summary>
    /// 検索結果から「フレームが連続している区間」だけを抜き出す。
    /// MainForm.CalcButton_Click から切り出した純粋関数。
    /// </summary>
    public static class ConsecutiveFilter
    {
        public static List<Types.FilterdStarterPokemonType> Filter(
            IReadOnlyList<Types.FilterdStarterPokemonType> results, int minCount)
        {
            var filtered = new List<Types.FilterdStarterPokemonType>();
            if (results.Count == 0 || minCount <= 0) return filtered;

            int runStart = 0;
            for (int i = 1; i <= results.Count; i++)
            {
                bool isContinuing = i < results.Count
                    && results[i].フレーム - results[i - 1].フレーム == 1;
                if (isContinuing) continue;

                // [runStart, i) が 1 つの連続区間
                if (i - runStart >= minCount)
                {
                    for (int j = runStart; j < i; j++) filtered.Add(results[j]);
                }
                runStart = i;
            }
            return filtered;
        }
    }
}
