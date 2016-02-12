using System.Collections.ObjectModel;

using PokeD.Core.Data.SCON;

namespace PokeD.SCON.UILibrary
{
    public partial class BasicUIViewModel
    {
        private ObservableCollection<Ban> bansGridDataList = new ObservableCollection<Ban>();
        public ObservableCollection<Ban> BansGridDataList { get { return bansGridDataList; } set { SetProperty(ref bansGridDataList, value); } } 
    }
}
