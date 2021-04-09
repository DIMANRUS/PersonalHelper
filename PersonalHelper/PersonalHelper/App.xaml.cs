﻿using PersonalHelper.Views;
using Xamarin.Forms;
using PersonalHelper.Models;
using System.Threading.Tasks;

namespace PersonalHelper {
    public partial class App : Application {
        public App() {
            InitializeComponent();
            _ = User.GetUserTheme() switch {
                "Dark" => Current.UserAppTheme = OSAppTheme.Dark,
                "Light" => Current.UserAppTheme = OSAppTheme.Light,
                _ => Current.UserAppTheme = OSAppTheme.Dark
            };
            if (User.GetUserName() == "null")
                MainPage = new NavigationPage(new Auth()) {
                    BarBackgroundColor = (User.GetUserTheme() == "Light") ? Color.White : Color.Black,
                    BarTextColor = (User.GetUserTheme() == "Light") ? Color.Black : Color.White
                };
            else
                MainPage = new NavigationPage(new MainPage()) {
                    BarBackgroundColor = (User.GetUserTheme() == "Light") ? Color.White : Color.Black,
                    BarTextColor = (User.GetUserTheme() == "Light") ? Color.Black : Color.White
                };

        }
        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}