namespace PokeD.SCON.UILibrary
{
    public partial class BasicUIViewModel
    {
        public bool SaveCredentials { get; private set; } = false;


        private void OnCheckBoxSaveCredentials(object obj)
        {
            switch (obj.ToString())
            {
                case "SaveCredentials":
                    SaveCredentials = !SaveCredentials;
                    break;
            }
        }
    }
}
