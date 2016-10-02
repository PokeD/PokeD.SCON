using System.Collections.ObjectModel;

using PokeD.SCON.UILibrary.GridModels;

namespace PokeD.SCON.UILibrary
{
    public partial class BasicUIViewModel
    {
        private ObservableCollection<PlayersDataGridModel> playersGridDataList = new ObservableCollection<PlayersDataGridModel>();
        public ObservableCollection<PlayersDataGridModel> PlayersGridDataList { get { return playersGridDataList; } set { SetProperty(ref playersGridDataList, value); } } 
    }
}
