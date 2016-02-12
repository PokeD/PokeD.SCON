using System;
using System.Collections.ObjectModel;

namespace PokeD.SCON.UILibrary
{
    public partial class BasicUIViewModel
    {
        public event Action<int> OnGetLog;
        
        private bool isLogsButtonVisible;
        public bool IsLogsButtonVisible { get { return isLogsButtonVisible; } set { SetProperty(ref isLogsButtonVisible, value); } }
        
        private ObservableCollection<LogsDataGridModel> logsGridDataList = new ObservableCollection<LogsDataGridModel>();
        public ObservableCollection<LogsDataGridModel> LogsGridDataList { get { return logsGridDataList; } set { SetProperty(ref logsGridDataList, value); } }


        private void OnButtonClickLogs(object obj)
        {
            switch (obj.ToString())
            {
                case "GetLog":
                    OnGetLog?.Invoke(TabItemSelectedIndex);
                    break;
            }
        }
    }
}
