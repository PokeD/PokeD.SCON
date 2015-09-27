using System.Collections.Generic;

using EmptyKeys.UserInterface;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;


namespace GameUILibrary
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
        private string serverIP;
        public string ServerIP { get { return serverIP; } set { SetProperty(ref serverIP, value); } }

        private string serverPort;
        public string ServerPort { get { return serverPort; } set { SetProperty(ref serverPort, value); } }

        private string scon_Password;
        public string SCON_Password { get { return scon_Password; } set { SetProperty(ref scon_Password, value); } }

        private bool autoReconnect;

        public ICommand CheckBoxCommand { get; set; }
        public ICommand ButtonCommand { get; set; }

        private bool connectButtonVisible = true;
        public InvertableBool ConnectButtonVisible { get { return connectButtonVisible; } set { SetProperty(ref connectButtonVisible, value); } }

        private string lastRefresh = "Last Refresh:";
        public string LastRefresh { get { return lastRefresh; } set { SetProperty(ref lastRefresh, value); } }


        private List<PlayersDataGridModel> playersGridDataList;
        public List<PlayersDataGridModel> PlayersGridDataList { get { return playersGridDataList; } set { SetProperty(ref playersGridDataList, value); } }


        private List<LogsDataGridModel> logsGridDataList;
        public List<LogsDataGridModel> LogsGridDataList { get { return logsGridDataList; } set { SetProperty(ref logsGridDataList, value); } }


        public BasicUIViewModel()
        {
            CheckBoxCommand = new RelayCommand(OnCheckBox);
            ButtonCommand = new RelayCommand(OnButtonClick);

            PlayersGridDataList = new List<PlayersDataGridModel>()
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
                    if (ushort.TryParse(serverPort, out port))
                        ConnectButtonVisible = !OnConnect(serverIP, port, scon_Password, autoReconnect);
                    else
                        MessageBox.Show("Invalid Port!", new RelayCommand(o => { }), false);

                    break;

                case "Disconnect":
                    ConnectButtonVisible = OnDisconnect();
                    break;

                case "Refresh":
                    OnRefresh();
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


        public bool OnConnect(string ip, ushort port, string password, bool autoReconnect = false)
        {
            return true;
        }

        public bool OnDisconnect()
        {
            return true;
        }

        public void OnRefresh()
        {

        }
    }
}
