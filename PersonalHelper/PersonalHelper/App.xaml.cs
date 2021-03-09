using PersonalHelper.Views;
using Xamarin.Forms;
using PersonalHelper.Models;

namespace PersonalHelper {
    public partial class App : Application {
        public App() {
            InitializeComponent();
            _ = User.GetUserTheme() switch {
                "Dark" => Current.UserAppTheme = OSAppTheme.Dark,
                "Light" => Current.UserAppTheme = OSAppTheme.Light,
                _ => Current.UserAppTheme = OSAppTheme.Unspecified
            };
            if (User.GetUserName() == "null")
                MainPage = new Auth();
            else
                MainPage = new MainPage();
        }
        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}