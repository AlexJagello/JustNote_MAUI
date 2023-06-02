using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace JustNote_maui.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        private ICommand clearDataBaseCommand;
        private bool isDarkTheme = (Application.Current.RequestedTheme == AppTheme.Dark);

        public ICommand ClearDataBaseCommand
        {
            get => clearDataBaseCommand;
        }

        public bool IsDarkTheme
        {
            get => isDarkTheme;
            set
            {
                //if (value == isDarkTheme) return;

                isDarkTheme = value;
                Application.Current.UserAppTheme = isDarkTheme ? AppTheme.Dark : AppTheme.Light;
                OnPropertyChanged();
            }
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
