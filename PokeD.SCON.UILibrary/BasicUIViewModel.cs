﻿using System;

using EmptyKeys.UserInterface;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Mvvm;

namespace PokeD.SCON.UILibrary
{
    public partial class BasicUIViewModel : ViewModelBase
    {
        public ICommand CheckBoxCommand { get; set; }
        private Action<object> OnCheckBox { get; }

        public ICommand ButtonCommand { get; set; }
        private Action<object> OnButtonClick { get; }
        
        public event Action<string, string> OnSaveLog;


        public BasicUIViewModel()
        {
            OnCheckBox += OnCheckBoxConnection;
            OnCheckBox += OnCheckBoxConsole;

            OnButtonClick += OnButtonClickConnection;
            OnButtonClick += OnButtonClickLogs;
            OnButtonClick += OnButtonClickCrashLogs;

            CheckBoxCommand = new RelayCommand(OnCheckBox);
            ButtonCommand = new RelayCommand(OnButtonClick);


            WatermarkGotFocus = new RelayCommand(BasicUIViewModel_WatermarkGotFocus);
            WatermarkLostFocus = new RelayCommand(BasicUIViewModel_WatermarkLostFocus);


            LogsGridDataList.CollectionChanged += (s, a) => IsLogsButtonVisible = LogsGridDataList.Count != 0;
            CrashLogsGridDataList.CollectionChanged += (s, a) => IsCrashLogsButtonVisible = LogsGridDataList.Count != 0;
        }
        
        public void Update(double elapsedTime) { }
        
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
