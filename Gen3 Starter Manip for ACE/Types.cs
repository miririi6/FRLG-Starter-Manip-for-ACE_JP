using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gen3_Starter_Manip_for_ACE
{
    public static class Types
    {
        public enum RomVersionType : byte
        {
            FireRed, LeafGreen
        }
        public enum NatureType : byte
        {
            [Description("がんばりや")] Hardy, [Description("さみしがり")] Lonely, [Description("ゆうかん")] Brave, [Description("いじっぱり")] Adamant, [Description("やんちゃ")] Naughty,
            [Description("ずぶとい")] Bold, [Description("すなお")]Docile, [Description("のんき")] Relaxed, [Description("わんぱく")] Impish, [Description("のうてんき")] Lax,
            [Description("おくびょう")] Timid, [Description("せっかち")] Hasty, [Description("まじめ")] Serious, [Description("ようき")] Jolly, [Description("むじゃき")] Naive,
            [Description("ひかえめ")] Modest, [Description("おっとり")] Mild, [Description("れいせい")] Quiet, [Description("てれや")] Bashful, [Description("うっかりや")] Rash,
            [Description("おだやか")] Calm, [Description("おとなしい")] Gentle, [Description("なまいき")] Sassy, [Description("しんちょう")] Careful, [Description("きまぐれ")] Quirky
        }
        public readonly struct NatureEffect
        {
            public NatureType Name { get; init; }
            public StatusType UpStatus { get; init; }
            public StatusType DownStatus { get; init; }
        }
        public enum StatusType : byte//現状、性格補正用
        {
            H, A, B, C, D, S, None
        }
        public struct IVs//個体値用
        {
            public int H { get; set; }
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
            public int D { get; set; }
            public int S { get; set; }
        }
        public enum StarterPokemonType : byte
        {
            Bulbasaur, Charmander, Squirtle
        }
        public enum SeedPokemonType : byte
        {
            Balbasaur, Ivysaur, Venusaur,
            Charmander, Charmeleon, Charizard,
            Squirtle, Wartortle, Blastoise
        }
        public readonly struct StarterPokemonBaseStats
        {
            public StarterPokemonType Name { get; init; }
            public int H { get; init; }
            public int A { get; init; }
            public int B { get; init; }
            public int C { get; init; }
            public int D { get; init; }
            public int S { get; init; }
        }
        public readonly struct WordDataType
        {
            public ushort ID { get; init; }
            public string Group { get; init; }
            public string Text { get; init; }
        }
        public readonly struct CorruptedPokemon
        {
            public ushort ID { get; init; }
            public string Text { get; init; }
            public string Pattern { get; init; }
        }
        public struct FilterdStarterPokemonType
        {
            public int フレーム { get; set; }
            public double 時間 { get; set; }
            public Types.NatureType 性格 { get; set; }
            public uint PID { get; set; }
            public int H { get; set; }
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
            public int D { get; set; }
            public int S { get; set; }
            public string 性別 { get; set; }
            public int EXP { get; set; }
        }
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}