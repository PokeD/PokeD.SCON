using System.Collections.ObjectModel;

using PokeD.Core.Data.SCON;

namespace PokeD.SCON.UILibrary
{
    public partial class BasicUIViewModel
    {
        private ObservableCollection<PlayerInfo> playersGridDataList = new ObservableCollection<PlayerInfo>();
        public ObservableCollection<PlayerInfo> PlayersGridDataList { get { return playersGridDataList; } set { SetProperty(ref playersGridDataList, value); } } 
    }
}
