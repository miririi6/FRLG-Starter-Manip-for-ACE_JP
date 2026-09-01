using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Packaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Gen3_Starter_Manip_for_ACE.MainForm;

namespace Gen3_Starter_Manip_for_ACE
{
    public static class SearchEngine
    {
        public static List<Types.FilterdStarterPokemonType> SearchPokemons(ConfigData config, uint seed)
        {
            ushort tid = (ushort)seed;
            for (int i = 0; i < config.minFrame - 1; i++)
            {
                seed = seed * Constants.MUL + Constants.ADD;
            }

            List<Types.FilterdStarterPokemonType> filterdStarterPokemonList = new();
            for (int i = 0; i < config.maxFrame - config.minFrame; i++)
            {
                seed = seed * Constants.MUL + Constants.ADD;

                Types.IVs iv = GetIVs(seed);
                uint pid = GetPID(seed);

                Types.NatureType nature = (Types.NatureType)(pid % 25);
                if (!config.checkedNatures.Contains(nature)) continue;

                if (config.isSearchForACE)
                {
                    //uint subOrderNum = pid % 24;
                    if (!Constants.expIn2ndIndex.Contains(pid % 24)) continue;
                }

                var effect = Constants.natureEffects[(int)nature];
                var upStatus = effect.UpStatus;
                var downStatus = effect.DownStatus;

                int Hreq = config.requiredHIV;
                int Areq = upStatus == Types.StatusType.A ? config.requiredAIV[2] : downStatus == Types.StatusType.A ? config.requiredAIV[0] : config.requiredAIV[1];
                int Breq = upStatus == Types.StatusType.B ? config.requiredBIV[2] : downStatus == Types.StatusType.B ? config.requiredBIV[0] : config.requiredBIV[1];
                int Creq = upStatus == Types.StatusType.C ? config.requiredCIV[2] : downStatus == Types.StatusType.C ? config.requiredCIV[0] : config.requiredCIV[1];
                int Dreq = upStatus == Types.StatusType.D ? config.requiredDIV[2] : downStatus == Types.StatusType.D ? config.requiredDIV[0] : config.requiredDIV[1];
                int Sreq = upStatus == Types.StatusType.S ? config.requiredSIV[2] : downStatus == Types.StatusType.S ? config.requiredSIV[0] : config.requiredSIV[1];

                if (iv.H < Hreq || iv.A < Areq || iv.B < Breq || iv.C < Creq || iv.D < Dreq || iv.S < Sreq) continue;

                ushort key = (ushort)((ushort)pid ^ tid);

                List<Types.CorruptedPokemon> targetCorruptedPokemons = new();
                int minExp = 65535;
                if (config.isSearchForACE)
                {
                    targetCorruptedPokemons = GetGeneratableCorruptedPokemons(key);
                    if (targetCorruptedPokemons.Count == 0)
                    {
                        continue;
                    }

                    //経験値が基準を満たしているか確認
                    minExp = GetMinimumExpForCorruption(config, key, targetCorruptedPokemons);

                    if (config.maxExp < minExp)
                    {
                        continue;
                    }
                }

                string sex = (pid & 0xFF) <= 30 ? "♀" : "♂";

                filterdStarterPokemonList.Add(new Types.FilterdStarterPokemonType
                {
                    フレーム = config.minFrame + i,
                    時間 = (config.minFrame + i) * 0.01674270646,
                    性格 = nature,
                    性格値 = pid,
                    H = iv.H,
                    A = iv.A,
                    B = iv.B,
                    C = iv.C,
                    D = iv.D,
                    S = iv.S,
                    性別 = sex,
                    経験値 = minExp.ToString(),
                });
            }
            return filterdStarterPokemonList;
        }

        public static List<Types.FilterdStarterPokemonType> SerchAroundFrames(ConfigData config, ushort tid, int currentFrame)
        {
            int minFrame = currentFrame - 50;
            int maxFrame = currentFrame + 50;

            uint seed = tid;

            for (int i = 0; i < minFrame; i++)
            {
                seed = seed * Constants.MUL + Constants.ADD;
            }

            List<Types.FilterdStarterPokemonType> pokeList = new();
            for (int i = 0; i < maxFrame - minFrame; i++)
            {
                seed = seed * Constants.MUL + Constants.ADD;
                Types.IVs iv = GetIVs(seed);
                uint pid = GetPID(seed);

                Types.NatureType nature = (Types.NatureType)(pid % 25);

                List<Types.CorruptedPokemon> targetCorruptedPokemons = new();
                int minExp = 65535;
                if (config.isSearchForACE)
                {
                    ushort key = (ushort)((ushort)pid ^ tid);
                    if (Constants.expIn2ndIndex.Contains(pid % 24))
                    {
                        targetCorruptedPokemons = GetGeneratableCorruptedPokemons(key);

                        //経験値が基準を満たしているか確認
                        int min = GetMinimumExpForCorruption(config, key, targetCorruptedPokemons);
                        if (min <= config.maxExp)
                        {
                            minExp = min;
                        }

                    }
                }

                string sex = (pid & 0xFF) <= 30 ? "♀" : "♂";
                pokeList.Add(new Types.FilterdStarterPokemonType() {
                    フレーム = minFrame + i + 1,
                    時間 = (minFrame + i + 1) * 0.01674270646,
                    性格値 = pid, H = iv.H,
                    A = iv.A,
                    B = iv.B,
                    C = iv.C,
                    D = iv.D,
                    S = iv.S,
                    性別 = sex,
                    性格 = nature,
                    経験値 = minExp.ToString() });
            }
            return pokeList;
        }

        public static List<Types.WordExpDataType> GetWordExpData(ConfigData config, ushort tid, uint pid)
        {
            ushort key = (ushort)((ushort)pid ^ tid);


            List<Types.WordExpDataType> wordExpDataList = new();
            if (Constants.expIn2ndIndex.Contains(pid % 24))
            {
                List<Types.CorruptedPokemon> targetCorruptedPokemons = new();
                targetCorruptedPokemons = GetGeneratableCorruptedPokemons(key);
                GetMinimumExpForCorruption(config, key, targetCorruptedPokemons);
                foreach (var poke in targetCorruptedPokemons)
                {
                    ushort checksumDiff = (ushort)(poke.ID - ((ushort)config.seedPokemon + 1));
                    foreach (var word in Resources.wordData)
                    {
                        ushort dec_v = (ushort)(key ^ word.ID);
                        int adj = checksumDiff + dec_v;
                        if ((config.minExp2 <= adj || ((adj - config.minExp) % 2 == 0 && config.minExp <= adj)) && adj <= config.maxExp)
                        {
                            wordExpDataList.Add(new Types.WordExpDataType { 経験値 = adj, ワード3 = poke.Text, ワード5 = word.Text, パターン = poke.Pattern });
                        }
                    }
                    foreach (var pokeWord in Resources.pokeWordData)
                    {
                        ushort dec_v = (ushort)(key ^ pokeWord.ID);
                        int adj = checksumDiff + dec_v;
                        if ((config.minExp2 <= adj || ((adj - config.minExp) % 2 == 0 && config.minExp <= adj)) && adj <= config.maxExp)
                        {
                            wordExpDataList.Add(new Types.WordExpDataType { 経験値 = adj, ワード3 = poke.Text, ワード5 = pokeWord.Text, パターン = poke.Pattern });
                        }
                    }
                }
                wordExpDataList.Sort((a, b) => a.経験値.CompareTo(b.経験値));
            }
            return wordExpDataList;
        }
        internal static Types.IVs GetIVs(uint seed)
        {
            seed = seed * Constants.MUL + Constants.ADD;
            seed = seed * Constants.MUL + Constants.ADD;

            ushort val1 = (ushort)(seed >> 16);
            ushort val2 = (ushort)((seed * Constants.MUL + Constants.ADD) >> 16);

            Types.IVs iv = new Types.IVs();

            // --- 1枚目の乱数(val1)から抽出 ---
            iv.H = (val1 >> 0) & 0x1F; //  0～ 4bit
            iv.A = (val1 >> 5) & 0x1F; //  5～ 9bit
            iv.B = (val1 >> 10) & 0x1F; // 10～14bit

            // --- 2枚目の乱数(val2)から抽出 ---
            iv.S = (val2 >> 0) & 0x1F; //  0～ 4bit
            iv.C = (val2 >> 5) & 0x1F; //  5～ 9bit
            iv.D = (val2 >> 10) & 0x1F; // 10～14bit

            return iv;
        }
        internal static uint GetPID(uint seed)
        {
            uint pid = seed >> 16;
            seed = seed * Constants.MUL + Constants.ADD;
            pid += ((seed >> 16) << 16);
            return pid;
        }

        static List<Types.CorruptedPokemon> GetGeneratableCorruptedPokemons(ushort key)
        {
            List<Types.CorruptedPokemon> targetCorruptedPokemons = new();
            foreach (var corPoke in Resources.corruptedPokemonData)
            {
                foreach (var word in Resources.wordData)
                {
                    if (corPoke.ID == (((key ^ word.ID) << 16) >> 16))
                    {
                        targetCorruptedPokemons.Add(new Types.CorruptedPokemon { ID = corPoke.ID, Text = word.Text, Pattern = corPoke.Pattern });
                    }
                }
                foreach (var pokeWord in Resources.pokeWordData)
                {
                    if (corPoke.ID == (((key ^ pokeWord.ID) << 16) >> 16))
                    {
                        targetCorruptedPokemons.Add(new Types.CorruptedPokemon { ID = corPoke.ID, Text = pokeWord.Text, Pattern = corPoke.Pattern });
                    }
                }
            }
            return targetCorruptedPokemons;
        }
        static int GetMinimumExpForCorruption(ConfigData config, ushort key, List<Types.CorruptedPokemon> pokes)
        {
            int minExp = 65535;
            foreach (var poke in pokes)
            {
                ushort checksumDiff = (ushort)(poke.ID - ((ushort)config.seedPokemon + 1));
                foreach (var word in Resources.wordData)
                {
                    ushort dec_v = (ushort)(key ^ word.ID);
                    int adj = checksumDiff + dec_v;
                    if (config.minExp2 <= adj || ((adj - config.minExp) % 2 == 0 && config.minExp <= adj))
                    {
                        minExp = Math.Min(adj, minExp);
                    }
                }
                foreach (var pokeWord in Resources.pokeWordData)
                {
                    ushort dec_v = (ushort)(key ^ pokeWord.ID);
                    int adj = checksumDiff + dec_v;
                    if (config.minExp2 <= adj || ((adj - config.minExp) % 2 == 0 && config.minExp <= adj))
                    {
                        minExp = Math.Min(adj, minExp);
                    }
                }
            }
            return minExp;
        }
    }
}
