using System;

using Aragas.Network.Data;

namespace PokeD.SCON.UILibrary.GridModels
{
    public class PlayersDataGridModel
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public ushort Ping { get; set; }
        public Vector3 Position { get; set; }
        public string LevelFile { get; set; }
        public TimeSpan PlayTime { get; set; }
    }
}
