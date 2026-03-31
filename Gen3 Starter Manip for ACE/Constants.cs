using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gen3_Starter_Manip_for_ACE.Types;

namespace Gen3_Starter_Manip_for_ACE
{
    public static class Constants
    {
        public static readonly NatureEffect[] natureEffects =
        {
            new (){Name = NatureType.Hardy, UpStatus = StatusType.None, DownStatus = StatusType.None},
            new (){Name = NatureType.Lonely, UpStatus = StatusType.A, DownStatus = StatusType.B},
            new (){Name = NatureType.Brave, UpStatus = StatusType.A, DownStatus = StatusType.S},
            new (){Name = NatureType.Adamant, UpStatus = StatusType.A, DownStatus = StatusType.C},
            new (){Name = NatureType.Naughty, UpStatus = StatusType.A, DownStatus = StatusType.D},
            new (){Name = NatureType.Bold, UpStatus = StatusType.B, DownStatus = StatusType.A},
            new (){Name = NatureType.Docile, UpStatus = StatusType.None, DownStatus = StatusType.None},
            new (){Name = NatureType.Relaxed, UpStatus = StatusType.B, DownStatus = StatusType.S},
            new (){Name = NatureType.Impish, UpStatus = StatusType.B, DownStatus = StatusType.C},
            new (){Name = NatureType.Lax, UpStatus = StatusType.B, DownStatus = StatusType.D},
            new (){Name = NatureType.Timid, UpStatus = StatusType.S, DownStatus = StatusType.A},
            new (){Name = NatureType.Hasty, UpStatus = StatusType.S, DownStatus = StatusType.B},
            new (){Name = NatureType.Serious, UpStatus = StatusType.None, DownStatus = StatusType.None},
            new (){Name = NatureType.Jolly, UpStatus = StatusType.S, DownStatus = StatusType.C},
            new (){Name = NatureType.Naive, UpStatus = StatusType.S, DownStatus = StatusType.D},
            new (){Name = NatureType.Modest, UpStatus = StatusType.C, DownStatus = StatusType.A},
            new (){Name = NatureType.Mild, UpStatus = StatusType.C, DownStatus = StatusType.B},
            new (){Name = NatureType.Quiet, UpStatus = StatusType.C, DownStatus = StatusType.S},
            new (){Name = NatureType.Bashful, UpStatus = StatusType.None, DownStatus = StatusType.None},
            new (){Name = NatureType.Rash, UpStatus = StatusType.C, DownStatus = StatusType.D},
            new (){Name = NatureType.Calm, UpStatus = StatusType.D, DownStatus = StatusType.A},
            new (){Name = NatureType.Gentle, UpStatus = StatusType.D, DownStatus = StatusType.B},
            new (){Name = NatureType.Sassy, UpStatus = StatusType.D, DownStatus = StatusType.S},
            new (){Name = NatureType.Careful, UpStatus = StatusType.D, DownStatus = StatusType.C},
            new (){Name = NatureType.Quirky, UpStatus = StatusType.None, DownStatus = StatusType.None}
        };
        public static readonly StarterPokemonBaseStats[] starterPokemonBaseStats =
        {
            new() {Name = StarterPokemonType.Bulbasaur, H = 45, A = 49, B = 49, C = 65, D = 65, S = 45},
            new() {Name = StarterPokemonType.Charmander, H = 39, A = 52, B = 43, C = 60, D = 50, S = 65},
            new() {Name = StarterPokemonType.Squirtle, H = 44, A = 48, B = 65, C = 50, D = 64, S = 43}
        };
        public const uint MUL = 0x41C64E6D;
        public const uint ADD = 0x6073;
        public static readonly uint[] expIn2ndIndex = { 6, 7, 12, 13, 18, 19 };
    }
}