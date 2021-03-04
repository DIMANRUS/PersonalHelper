using PersonalHelper.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PersonalHelper.ViewModels {
    partial class MainPageVM : ICommand {
        public MainPageVM() {
            OpenSettings = new Command(execute: async () => { /*await Application.Current.MainPage.Navigation.PushAsync(new Settings());*/
                HttpClient http = new HttpClient();
                string result = await http.GetStringAsync("https://api.personalhelper.dimanrus.ru/api/weather/%D0%9C%D0%BE%D1%81%D0%BA%D0%B2%D0%B0");
                await CurrentPage.DisplayAlert("Привет", result, "OK");
            });
        }
        private Page CurrentPage{ get => Application.Current.MainPage; }
        public ICommand OpenSettings { private set; get; }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) {
            throw new NotImplementedException();
        }
        public void Execute(object parameter) {
            throw new NotImplementedException();
        }
    }
}