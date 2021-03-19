using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalHelper.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage {
        public Settings() {
            InitializeComponent();
        }
        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            switch (((RadioButton)sender).Content)
            {
                case "Тёмный":
                    Preferences.Set("UserTheme", "Dark");
                    Application.Current.UserAppTheme = OSAppTheme.Dark;
                    break;
                case "Светлый":
                    Preferences.Set("UserTheme", "Light");
                    Application.Current.UserAppTheme = OSAppTheme.Light;
                    break;
            }
        }
    }
}