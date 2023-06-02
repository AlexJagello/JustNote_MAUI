using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNote_maui
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            Application.Current.RequestedThemeChanged += (s, a) =>
            {
                // Respond to the theme change
            };
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if(e.Value)
                Application.Current.UserAppTheme = AppTheme.Dark;
            else Application.Current.UserAppTheme = AppTheme.Light;
            
        }
    }
}