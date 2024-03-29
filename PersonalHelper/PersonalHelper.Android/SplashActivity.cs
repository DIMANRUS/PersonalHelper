﻿using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.AppCompat.App;
using System.Threading.Tasks;

namespace PersonalHelper.Droid {
    [Activity(Label = "Личный помощник",Icon = "@mipmap/icon",Theme = "@style/Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState) =>
            base.OnCreate(savedInstanceState, persistentState);
        protected override void OnResume() {
            base.OnResume();
            Task.Run(() => SimulateStartup());
        }
        async void SimulateStartup() {
            await Task.Delay(1000);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}