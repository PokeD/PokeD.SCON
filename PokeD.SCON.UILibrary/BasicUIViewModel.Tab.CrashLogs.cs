using System;
using System.Collections.ObjectModel;

using PokeD.SCON.UILibrary.GridModels;

namespace PokeD.SCON.UILibrary
{
    public partial class BasicUIViewModel
    {
        public event Action<int> OnGetCrashLog;

        private bool isCrashLogsButtonVisible;
        public bool IsCrashLogsButtonVisible { get { return isCrashLogsButtonVisible; } set { SetProperty(ref isCrashLogsButtonVisible, value); } }
        
        private ObservableCollection<LogsDataGridModel> crashLogsGridDataList = new ObservableCollection<LogsDataGridModel>();
        public ObservableCollection<LogsDataGridModel> CrashLogsGridDataList { get { return crashLogsGridDataList; } set { SetProperty(ref crashLogsGridDataList, value); } }


        private void OnButtonClickCrashLogs(object obj)
        {
            switch (obj.ToString())
            {
                case "GetCrashLog":
                    OnGetCrashLog?.Invoke(TabItemSelectedIndex);
                    break;
            }
        }
    }
}
