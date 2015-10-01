using System;
using System.Collections.ObjectModel;

using EmptyKeys.UserInterface;
using EmptyKeys.UserInterface.Controls;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;

namespace PokeD.SCON.UILibrary
{
    public class InvertableBool
    {
        private readonly bool _value;

        public bool Value => _value;
        public bool Invert => !_value;

        public InvertableBool(bool b)
        {
            _value = b;
        }

        public static implicit operator InvertableBool(bool b)
        {
            return new InvertableBool(b);
        }

        public static implicit operator bool (InvertableBool b)
        {
            return b._value;
        }

    }

    public class BasicUIViewModel : ViewModelBase
    {
        public ICommand NumericPreviewTextInput { get; set; }

        #region Watermark

        public ICommand WatermarkGotFocus { get; set; }
        public ICommand WatermarkLostFocus { get; set; }

        private Visibility serverIP_Watermark;
        public Visibility ServerIP_Watermark { get { return serverIP_Watermark; } set { SetProperty(ref serverIP_Watermark, value); } }

        private Visibility serverPort_Watermark;
        public Visibility ServerPort_Watermark { get { return serverPort_Watermark; } set { SetProperty(ref serverPort_Watermark, value); } }

        private Visibility scon_Password_Watermark;
        public Visibility SCON_Password_Watermark { get { return scon_Password_Watermark; } set { SetProperty(ref scon_Password_Watermark, value); } }

        private void BasicUIViewModel_WatermarkGotFocus(object obj)
        {
            if (obj == null)
                return;

            switch (obj.ToString())
            {
                case "ServerIP":
                    ServerIP_Watermark = Visibility.Hidden;
                    break;

                case "ServerPort":
                    ServerPort_Watermark = Visibility.Hidden;
                    break;

                case "SCON_Password":
                    SCON_Password_Watermark = Visibility.Hidden;
                    break;
            }
        }
        private void BasicUIViewModel_WatermarkLostFocus(object obj)
        {
            if (obj == null)
                return;

            switch (obj.ToString())
            {
                case "ServerIP":
                    ServerIP_Watermark = string.IsNullOrEmpty(serverIP) ? Visibility.Visible : Visibility.Hidden;
                    break;

                case "ServerPort":
                    ServerPort_Watermark = string.IsNullOrEmpty(serverPort) ? Visibility.Visible : Visibility.Hidden;
                    break;

                case "SCON_Password":
                    SCON_Password_Watermark = string.IsNullOrEmpty(scon_Password) ? Visibility.Visible : Visibility.Hidden;
                    break;
            }
        }

        #endregion Watermark


        public event Func<string, ushort, string, bool, bool> OnConnect;
        public event Func<bool> OnDisconnect;
        public event Func<bool> OnRefresh;


        private string serverIP = string.Empty;
        public string ServerIP { get { return serverIP; } set { SetProperty(ref serverIP, value); } }

        private string serverPort = string.Empty;
        public string ServerPort { get { return serverPort; } set { SetProperty(ref serverPort, value); } }

        private string scon_Password = string.Empty;

        public string SCON_Password
        {
            get
            {
                return scon_Password;
            }
            set
            {
                SetProperty(ref scon_Password, value);
            }
        }

        private bool autoReconnect;

        public ICommand CheckBoxCommand { get; set; }
        public ICommand ButtonCommand { get; set; }

        private bool isConnectButtonVisible = true;
        public InvertableBool IsConnectButtonVisible { get { return isConnectButtonVisible; } set { SetProperty(ref isConnectButtonVisible, value); } }

        private TimeSpan lastRefresh;
        public TimeSpan LastRefresh { get { return lastRefresh; } set { SetProperty(ref lastRefresh, value); } }


        public event Action<string> TabChanged;


        private ObservableCollection<PlayersDataGridModel> playersGridDataList;
        public ObservableCollection<PlayersDataGridModel> PlayersGridDataList { get { return playersGridDataList; } set { SetProperty(ref playersGridDataList, value); } }


        private ObservableCollection<BansDataGridModel> bansGridDataList;
        public ObservableCollection<BansDataGridModel> BansGridDataList { get { return bansGridDataList; } set { SetProperty(ref bansGridDataList, value); } }


        private ObservableCollection<PlayersDatabaseDataGridModel> playersDatabaseGridDataList;
        public ObservableCollection<PlayersDatabaseDataGridModel> PlayersDatabaseGridDataList { get { return playersDatabaseGridDataList; } set { SetProperty(ref playersDatabaseGridDataList, value); } }


        private ObservableCollection<LogsDataGridModel> logsGridDataList;
        public ObservableCollection<LogsDataGridModel> LogsGridDataList { get { return logsGridDataList; } set { SetProperty(ref logsGridDataList, value); } }

        private ObservableCollection<LogsDataGridModel> crashLogsGridDataList;
        public ObservableCollection<LogsDataGridModel> CrashLogsGridDataList { get { return crashLogsGridDataList; } set { SetProperty(ref crashLogsGridDataList, value); } }

        private bool isLogsButtonVisible;
        public bool IsLogsButtonVisible { get { return isLogsButtonVisible; } set { SetProperty(ref isLogsButtonVisible, value); } }

        private bool isCrashLogsButtonVisible;
        public bool IsCrashLogsButtonVisible { get { return isCrashLogsButtonVisible; } set { SetProperty(ref isCrashLogsButtonVisible, value); } }


        private TabItem tabSelectedIndex;
        public TabItem TabSelectedItem
        {
            get
            {
                return tabSelectedIndex;
            }
            set
            {
                SetProperty(ref tabSelectedIndex, value);

                TabChanged?.Invoke((string) tabSelectedIndex.Header);
            }
        }


        private int tabItemSelectedIndex;
        public int TabItemSelectedIndex { get { return tabItemSelectedIndex; } set { SetProperty(ref tabItemSelectedIndex, value); } }


        public event Action<int> OnGetLog;
        public event Action<int> OnGetCrashLog;

        public event Action<string, string> OnSaveLog;


        public BasicUIViewModel()
        {
            CheckBoxCommand = new RelayCommand(OnCheckBox);
            ButtonCommand = new RelayCommand(OnButtonClick);

            NumericPreviewTextInput = new RelayCommand(BasicUIViewModel_NumericPreviewTextInput);

            WatermarkGotFocus = new RelayCommand(BasicUIViewModel_WatermarkGotFocus);
            WatermarkLostFocus = new RelayCommand(BasicUIViewModel_WatermarkLostFocus);

            PlayersGridDataList = new ObservableCollection<PlayersDataGridModel>();
            BansGridDataList = new ObservableCollection<BansDataGridModel>();
            PlayersDatabaseGridDataList = new ObservableCollection<PlayersDatabaseDataGridModel>();
            LogsGridDataList = new ObservableCollection<LogsDataGridModel>();
            CrashLogsGridDataList = new ObservableCollection<LogsDataGridModel>();

            LogsGridDataList.CollectionChanged += (s, a) => IsLogsButtonVisible = LogsGridDataList.Count != 0;
            CrashLogsGridDataList.CollectionChanged += (s, a) => IsCrashLogsButtonVisible = LogsGridDataList.Count != 0;
        }

        private void BasicUIViewModel_NumericPreviewTextInput(object o)
        {

        }

        private void OnButtonClick(object obj)
        {
            if (obj == null)
                return;

            switch (obj.ToString())
            {
                case "Connect":
                    ushort port;
                    if (ushort.TryParse(serverPort, out port) && OnConnect != null)
                        IsConnectButtonVisible = !OnConnect(serverIP, port, scon_Password, autoReconnect);
                    else
                        MessageBox.Show("Invalid Port!", new RelayCommand(o => { }), false);

                    break;

                case "Disconnect":
                    if (OnDisconnect != null)
                        IsConnectButtonVisible = OnDisconnect();
                    break;

                case "Refresh":
                    LastRefresh = DateTime.Now.TimeOfDay;

                    OnRefresh?.Invoke();
                    break;

                case "GetLog":
                    OnGetLog?.Invoke(TabItemSelectedIndex);
                    break;

                case "GetCrashLog":
                    OnGetCrashLog?.Invoke(TabItemSelectedIndex);
                    break;
            }
        }

        private void OnCheckBox(object obj)
        {
            if (obj == null)
                return;

            switch (obj.ToString())
            {
                case "AutoReconnect":
                    autoReconnect = !autoReconnect;
                    break;
            }
        }

        public void Update(double elapsedTime)
        {
        }


        public void DisplayLog(string logname, string log)
        {
            MessageBox.Show(log, "Save log?", MessageBoxButton.YesNo, new RelayCommand(button =>
            {
                if(button == null)
                    return;

                if (button.ToString() == "Yes")
                    OnSaveLog?.Invoke(logname, log);
                
            }), false);
        }

        public void DisplayMessage(string message)
        {
            MessageBox.Show(message, new RelayCommand(o => { }), false);
        }

    }
}
