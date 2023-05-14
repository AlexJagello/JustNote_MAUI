using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace JustNote_maui.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        private ICommand clearDataBaseCommand;


        public ICommand ClearDataBaseCommand
        {
            get => clearDataBaseCommand;
        }

        public SettingsPageViewModel() 
        {
            Title = "Settings";
            clearDataBaseCommand = new Command(ClearDB);
        }


        private void ClearDB()
        {
            App.RequestSimpleNote.Clear();
            App.RequestListNote.Clear();
            //App.Database.

            
        }

    }
}
