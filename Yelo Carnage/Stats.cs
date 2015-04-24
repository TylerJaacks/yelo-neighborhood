using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Yelo.Carnage
{
    //http://code.google.com/p/open-sauce/source/browse/OpenSauce/Halo2/Halo2_Xbox/Networking/Statistics.hpp
    public class Stats
    {
        //struct pcr_stat_player,
        //1.0 Address: 0x55CAF0

        private char[] data;
        private MemoryStream DataStream;
        private BinaryReader br;

        public char[] PlayerName { get { br.BaseStream.Position = 0; return br.ReadChars(16); } }
        public char[] DisplayName { get { br.BaseStream.Position = 0x20; return br.ReadChars(16); } }
        public char[] ScoreString { get { br.BaseStream.Position = 0x40; return br.ReadChars(16); } }

        public int Kills { get { br.BaseStream.Position = 0x60; return br.ReadInt32(); } }
        public int Deaths { get { br.BaseStream.Position = 0x64; return br.ReadInt32(); } }
        public int Assists { get { br.BaseStream.Position = 0x68; return br.ReadInt32(); } }
        public int Suicides { get { br.BaseStream.Position = 0x6C; return br.ReadInt32(); } }

        public short Place { get { br.BaseStream.Position = 0x70; return br.ReadInt16(); } }
        //UNKNOWN_TYPE(int16); // 0x72
        //UNKNOWN_TYPE(byte); PAD24; // 0x74
        //UNKNOWN_TYPE(int32); // 0x78

        public int MedalsEarned { get { br.BaseStream.Position = 0x7C; return br.ReadInt32(); } }
        public int /*Flags*/ MedalsEarnedByType { get { br.BaseStream.Position = 0x80; return br.ReadInt32(); } }

        public int TotalShots { get { br.BaseStream.Position = 0x84; return br.ReadInt32(); } }
        public int ShotsHit { get { br.BaseStream.Position = 0x88; return br.ReadInt32(); } }
        public int HeadShots { get { br.BaseStream.Position = 0x8C; return br.ReadInt32(); } }

        public int[] Killed { get { br.BaseStream.Position = 0x90; return GetIntArray(br.ReadBytes(16 * sizeof(int))); } }

        int[] GetIntArray(byte[] data)
        { 
            int[] output = new int[data.Length / sizeof(int)];
            for (int i = 0; i < output.Length; i += sizeof(int))
                output[i / sizeof(int)] = (data[i] << 24) | (data[i + 1] << 16) | (data[i + 2] << 8) | data[i + 3];
            return output;
        }

        //// 16 byte structure
        //UNKNOWN_TYPE(int32); // 0xD0
        //UNKNOWN_TYPE(int32); // 0xD4
        //UNKNOWN_TYPE(int32); // 0xD8
        //UNKNOWN_TYPE(int32); // 0xDC

        public char[] PlaceString { get { br.BaseStream.Position = 0xE0; return br.ReadChars(16); } }

        //// 12 byte structure
        //UNKNOWN_TYPE(int32); // 0x100
        //UNKNOWN_TYPE(int32); // 0x104
        //UNKNOWN_TYPE(byte); PAD24; // 0x108

        public int GameTypeValue0 { get { br.BaseStream.Position = 0x10C; return br.ReadInt32(); } }
        public int GameTypeValue1 { get { br.BaseStream.Position = 0x110; return br.ReadInt32(); } }

        public int FlagCarrierKills { get { return GameTypeValue0; } }
        public int FlagGrabs { get { return GameTypeValue1; } }

        public int AverageLife { get { return GameTypeValue0; } }
        public int MostKillsInARow { get { return GameTypeValue1; } }

        public int BallCarrierKills { get { return GameTypeValue0; } }
        public int KillsAsCarrier { get { return GameTypeValue1; } }

        public int TotalControlTime { get { return GameTypeValue0; } }
        public int TimeOnHill { get { return GameTypeValue1; } }

        public int JuggernautKills { get { return GameTypeValue0; } }
        public int KillsAsJuggernaut { get { return GameTypeValue1; } }

        public int TerritoriesTaken { get { return GameTypeValue0; } }
        public int TerritoriesLost { get { return GameTypeValue1; } }

        public int BombGrabs { get { return GameTypeValue0; } }
        public int BombCarrierKills { get { return GameTypeValue1; } }
       
        enum GameResultsStatistic
        {
            Games_Played,
            Games_quit,
            Games_Disconnected,
            Games_Completed,
            Games_Won,
            Games_Tied,
            Rounds_Won,

            Kills, // 0x7
            Assists,
            Deaths,
            Betrayals,
            Suicides,
            Most_Kills_In_A_Row,

            Seconds_alive, // 0xD
            CTF_Flag_Scores, // 0xE
            CTF_Flag_Grabs, // 0xF
            CTF_Flag_Carrier_Kills, // 0x10
            CTF_Flag_Returns, // 0x11
            CTF_Bomb_Scores, // 0x12
            CTF_Bomb_Plants, // 0x13
            CTF_Bomb_Carrier_Kills, // 0x14
            CTF_Bomb_Grabs, // 0x15
            CTF_Bomb_Returns, // 0x16
            Oddball_Time_With_Ball, // 0x17
            Oddball_Unused, // 0x18
            Oddball_Kills_as_Carrier, // 0x19
            Oddball_Ball_Carrier_Kills, // 0x1A
            KOTH_Time_on_hill, // 0x1B
            KOTH_Total_Control_Time, // 0x1C
            KOTH_Number_of_Controls, // unused
            KOTH_Longest_Control_Time, // unused
            Race_Laps, // unused
            Race_Total_Lap_Time, // unused
            Race_Fastest_Lap_Time, // unused
            Headhunter_Heads_Picked_Up, // unused
            Headhunter_Heads_Deposited, // unused
            Headhunter_Number_of_Deposits, // unused
            Headhunter_Largest_Deposit, // unused
            Juggernaut_Kills, // 0x26
            Juggernaut_Kills_as_Juggernaut, // 0x27
            Juggernaut_Total_Control_Time, // 0x28
            Juggernaut_Number_of_Controls, // unused
            Juggernaut_Longest_Control_Time, // unused
            Territories_Taken, // 0x2B
            Territories_Lost, // 0x2C
        }

        enum GameResultsDamage
        {
            Kills,
            Seaths,
            Betrayals,
            Suicides,
            Shots_fire,
            Shots_hit,
            Headshots,
        }

        enum GameResultsPlayervsPlayer
        {
            Kills,
            Deaths,
        }

        enum GameResultsMedal
        {
            Multi_Kill_2,
            Multi_Kill_3,
            Multi_Kill_4,
            Multi_Kill_5,
            Multi_Kill_6,
            Multi_Kill_7_or_more,

            Sniper_Kill, // 0x6
            Collision_Kill,
            Bash_Kill,
            Stealth_Kill,
            Killed_Vehicle,
            Boarded_Vehicle,
            Grenade_Stick,

            Five_Kills_in_a_row, // 0xD
            Ten_Kills_in_a_row,
            Fifteen_Kills_in_a_row,
            Twenty_Kills_in_a_row,
            Twenty_Five_Kills_in_a_row,

            CTF_Flag_Grab, // 0x12
            CTF_Flag_Carrier_Kill,
            CTF_Flag_Returned,
            CTF_Bomb_Planted,
            CTF_Bomb_Carrier_Kill,
            CTF_Bomb_Defused,
        }
    }
}
