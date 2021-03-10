using PersonalHelper.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PersonalHelper.Models;

namespace PersonalHelper.ViewModels {
    partial class MainPageVM : ICommand, INotifyPropertyChanged 
    {
        public MainPageVM() 
        {
            OpenSettings = new Command(execute: async () => {
                await CurrentPage.Navigation.PushModalAsync(new Settings(), true);
            });
            Task.Run(async () => { 
                NewsCollection = await newsModel.GetTopNews();
                NotifyPropertyChanged("NewsCollection");
            });
            OpenNews = new Command<string>(execute: async (string url) => {
                await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
            });
            OpenAllNews = new Command(execute: async () => {
                await CurrentPage.Navigation.PushModalAsync(new NewsPage());
            }); 
            OpenWeatherForOneDay = new Command(execute: async () =>
            {
                await CurrentPage.Navigation.PushModalAsync(new WeatherPage());
            });
        }
        private Page CurrentPage { get => Application.Current.MainPage; }
        public ICommand OpenSettings { private set; get; }
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool CanExecute(object parameter) 
        {
            throw new NotImplementedException();
        }
        public void Execute(object parameter) 
        {
            throw new NotImplementedException();
        }
    }
}