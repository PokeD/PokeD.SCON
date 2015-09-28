using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                    ServerIP_Watermark = string.IsNullOrEmpty(ServerIP) ? Visibility.Visible : Visibility.Hidden;
                    break;

                case "ServerPort":
                    ServerPort_Watermark = string.IsNullOrEmpty(ServerPort) ? Visibility.Visible : Visibility.Hidden;
                    break;

                case "SCON_Password":
                    SCON_Password_Watermark = string.IsNullOrEmpty(SCON_Password) ? Visibility.Visible : Visibility.Hidden;
                    break;
            }
        }

        #endregion Watermark

        public event Func<string, ushort, string, bool, bool> OnConnect;
        public event Func<bool> OnDisconnect;
        public event Func<bool> OnRefresh;


        private string serverIP;
        public string ServerIP { get { return serverIP; } set { SetProperty(ref serverIP, value); } }

        private string serverPort;
        public string ServerPort { get { return serverPort; } set { SetProperty(ref serverPort, value); } }

        private string scon_Password;
        public string SCON_Password { get { return scon_Password; } set { SetProperty(ref scon_Password, value); } }

        private bool autoReconnect;

        public ICommand CheckBoxCommand { get; set; }
        public ICommand ButtonCommand { get; set; }

        private bool isConnectButtonVisible = true;
        public InvertableBool IsConnectButtonVisible { get { return isConnectButtonVisible; } set { SetProperty(ref isConnectButtonVisible, value); } }

        private string lastRefresh = "Last Refresh:";
        public string LastRefresh { get { return lastRefresh; } set { SetProperty(ref lastRefresh, value); } }


        public event Action<string> TabChanged;


        private ObservableCollection<PlayersDataGridModel> playersGridDataList;
        public ObservableCollection<PlayersDataGridModel> PlayersGridDataList { get { return playersGridDataList; } set { SetProperty(ref playersGridDataList, value); } }


        private ObservableCollection<PlayersDatabaseDataGridModel> playersDatabaseGridDataList;
        public ObservableCollection<PlayersDatabaseDataGridModel> PlayersDatabaseGridDataList { get { return playersDatabaseGridDataList; } set { SetProperty(ref playersDatabaseGridDataList, value); } }


        private ObservableCollection<LogsDataGridModel> logsGridDataList;
        public ObservableCollection<LogsDataGridModel> LogsGridDataList { get { return logsGridDataList; } set { SetProperty(ref logsGridDataList, value); } }

        private ObservableCollection<LogsDataGridModel> crashLogsGridDataList;
        public ObservableCollection<LogsDataGridModel> CrashLogsGridDataList { get { return crashLogsGridDataList; } set { SetProperty(ref crashLogsGridDataList, value); } }


        private DataGrid dataGridProperty;
        public DataGrid DataGridProperty { get { return dataGridProperty; } set { SetProperty(ref dataGridProperty, value); } }


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

                if (TabChanged != null)
                    TabChanged((string) tabSelectedIndex.Header);
            }
        }


        private int tabItemSelectedIndex;
        public int TabItemSelectedIndex { get { return tabItemSelectedIndex; } set { SetProperty(ref tabItemSelectedIndex, value); } }


        public event Action<int> OnGetLog;
        public event Action<int> OnGetCrashLog;

        public BasicUIViewModel()
        {
            CheckBoxCommand = new RelayCommand(OnCheckBox);
            ButtonCommand = new RelayCommand(OnButtonClick);

            WatermarkGotFocus = new RelayCommand(BasicUIViewModel_WatermarkGotFocus);
            WatermarkLostFocus = new RelayCommand(BasicUIViewModel_WatermarkLostFocus);

            //DataGridProperty.Loaded += (s, e) => { // Column widths
            //    DataGridProperty.Columns.AsParallel().ForAll(column => {
            //        column.MinWidth = column.ActualWidth;
            //        column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            //    });
            //};

            PlayersGridDataList = new ObservableCollection<PlayersDataGridModel>
            {
                new PlayersDataGridModel { Number = 0, IP = "127.0.0.1", Ping = 5,  GameJoltID = 1483, Name = "Aragas", Online = true },
                new PlayersDataGridModel { Number = 1, IP = "227.0.0.1", Ping = 15, GameJoltID = 2483, Name = "Bragas", Online = false },
                new PlayersDataGridModel { Number = 2, IP = "327.0.0.1", Ping = 25, GameJoltID = 3483, Name = "Cragas", Online = true },
                new PlayersDataGridModel { Number = 3, IP = "427.0.0.1", Ping = 35, GameJoltID = 4483, Name = "Dragas", Online = false }
            };
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
                    if(OnRefresh != null)
                        OnRefresh();
                    break;

                case "GetLog":
                    if (OnGetLog != null)
                        OnGetLog(TabItemSelectedIndex);
                    break;

                case "GetCrashLog":
                    if (OnGetCrashLog != null)
                        OnGetCrashLog(TabItemSelectedIndex);
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


        public void DisplayLog(string log)
        {
            MessageBox.Show(log, new RelayCommand(o => {}), false);
        }

        public void DisplayMessage(string message)
        {
            MessageBox.Show(message, new RelayCommand(o => { }), false);
        }

    }
}
