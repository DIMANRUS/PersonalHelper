using PersonalHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PersonalHelper.ViewModels {
    class MainPageModel {
        public string UserName { get => GetUserName().ToString(); }
        public Wheather Wheather { get; set; }
        private async Task<string> GetUserName() => await SecureStorage.GetAsync("UserName");
    }
}