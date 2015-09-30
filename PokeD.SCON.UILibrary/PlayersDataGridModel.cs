using System;

namespace PokeD.SCON.UILibrary
{
    public class PlayersDataGridModel
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public ulong GameJoltID { get; set; }
        public string IP { get; set; }
        public uint Ping { get; set; }
        public string LevelFile { get; set; }
        public TimeSpan PlayTime { get; set; }
    }
}
