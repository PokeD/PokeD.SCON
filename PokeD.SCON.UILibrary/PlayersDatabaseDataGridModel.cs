using System;

namespace PokeD.SCON.UILibrary
{
    public class PlayersDatabaseDataGridModel
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public ulong GameJoltID { get; set; }
        public string LastIP { get; set; }
        public DateTime LastSeen { get; set; }
    }
}
