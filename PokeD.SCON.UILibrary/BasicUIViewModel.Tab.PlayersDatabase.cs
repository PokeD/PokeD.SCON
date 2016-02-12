using System.Collections.ObjectModel;

using PokeD.Core.Data.SCON;

namespace PokeD.SCON.UILibrary
{
    public partial class BasicUIViewModel
    {
        private ObservableCollection<PlayerDatabase> playersDatabaseGridDataList = new ObservableCollection<PlayerDatabase>();
        public ObservableCollection<PlayerDatabase> PlayersDatabaseGridDataList { get { return playersDatabaseGridDataList; } set { SetProperty(ref playersDatabaseGridDataList, value); } }
        
    }
}
