using System;

using EmptyKeys.UserInterface;
using EmptyKeys.UserInterface.Input;

namespace PokeD.SCON.UILibrary
{
    public partial class BasicUIViewModel
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
        public string SCON_Password { get { return scon_Password; } set { SetProperty(ref scon_Password, value); } }

        private bool isConnectButtonVisible = true;
        public InvertableBool IsConnectButtonVisible { get { return isConnectButtonVisible; } set { SetProperty(ref isConnectButtonVisible, value); } }

        private TimeSpan lastRefresh;
        public TimeSpan LastRefresh { get { return lastRefresh; } set { SetProperty(ref lastRefresh, value); } }
        
        private bool autoReconnect;


        private void OnButtonClickConnection(object obj)
        {
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
            }
        }

        private void OnCheckBoxConnection(object obj)
        {
            switch (obj.ToString())
            {
                case "AutoReconnect":
                    autoReconnect = !autoReconnect;
                    break;
            }
        }
    }
}
