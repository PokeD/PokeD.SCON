using System;

using EmptyKeys.UserInterface.Controls;

namespace PokeD.SCON.UILibrary
{
    public partial class BasicUIViewModel
    {
        public event Action<string> TabChanged;

        private TabItem tabSelectedIndex;
        public TabItem TabSelectedItem
        {
            get { return tabSelectedIndex; }
            set { SetProperty(ref tabSelectedIndex, value); TabChanged?.Invoke((string) tabSelectedIndex.Header); }
        }

        private int tabItemSelectedIndex;
        public int TabItemSelectedIndex { get { return tabItemSelectedIndex; } set { SetProperty(ref tabItemSelectedIndex, value); } }
    }
}
