using PersonalHelper.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PersonalHelper {
    public partial class App : Application {
        public App() {
            InitializeComponent();
            if (Preferences.Get("UserTheme", "null") == "Dark")
                Current.UserAppTheme = OSAppTheme.Dark;
            else if (Preferences.Get("UserTheme", "null") == "Light")
                Current.UserAppTheme = OSAppTheme.Light;
            else
                Current.UserAppTheme = OSAppTheme.Unspecified;
            if (Preferences.Get("UserName", "null") == "null")
                MainPage = new Auth();
            else
                MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}