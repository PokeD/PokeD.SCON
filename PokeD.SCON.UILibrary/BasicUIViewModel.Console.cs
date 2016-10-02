using System;

namespace PokeD.SCON.UILibrary
{
    public partial class BasicUIViewModel
    {
        public event Action<bool> OnChatStateChanged;
        private bool enableChat;

        private string consoleOutput = string.Empty;
        public string ConsoleOutput { get { return consoleOutput; } set { SetProperty(ref consoleOutput, value); } }


        private void OnCheckBoxConsole(object obj)
        {
            switch (obj.ToString())
            {
                case "EnableChat":
                    enableChat = !enableChat;
                    OnChatStateChanged?.Invoke(enableChat);
                    break;
            }
        }
    }
}
