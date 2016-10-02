using System.Collections.ObjectModel;

using PokeD.SCON.UILibrary.GridModels;

namespace PokeD.SCON.UILibrary
{
    public partial class BasicUIViewModel
    {
        private ObservableCollection<BansDataGridModel> bansGridDataList = new ObservableCollection<BansDataGridModel>();
        public ObservableCollection<BansDataGridModel> BansGridDataList { get { return bansGridDataList; } set { SetProperty(ref bansGridDataList, value); } } 
    }
}
