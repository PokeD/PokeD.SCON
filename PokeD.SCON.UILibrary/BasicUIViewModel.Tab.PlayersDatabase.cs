using System.Collections.ObjectModel;

using PokeD.SCON.UILibrary.GridModels;

namespace PokeD.SCON.UILibrary
{
    public partial class BasicUIViewModel
    {
        private ObservableCollection<PlayersDatabaseDataGridModel> playersDatabaseGridDataList = new ObservableCollection<PlayersDatabaseDataGridModel>();
        public ObservableCollection<PlayersDatabaseDataGridModel> PlayersDatabaseGridDataList { get { return playersDatabaseGridDataList; } set { SetProperty(ref playersDatabaseGridDataList, value); } } 
    }
}
