using System;

namespace PokeD.SCON.UILibrary.GridModels
{
    public class BansDataGridModel
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public DateTime BanTime { get; set; }
        public DateTime UnBanTime { get; set; }
        public string Reason { get; set; }
    }
}
