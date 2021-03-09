using PersonalHelper.Models;
using PersonalHelper.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PersonalHelper.ViewModels
{
    class NewsPageVM : ICommand, INotifyPropertyChanged {
        private readonly News newsModel = new News();
        public NewsPageVM() {
            BackToMainPage = new Command(execute: async () => { await CurrentPage.Navigation.PopModalAsync(); });
            Task.Run(async () => {
                NewsCityCollection = await newsModel.GetAllNewsForCity();
                NotifyPropertyChanged("NewsCityCollection");
            });
            OpenNews = new Command<string>(execute: async (string url) => {
                await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
            });
        }
        private Page CurrentPage { get => Application.Current.MainPage; }
        public ObservableCollection<Article> NewsCityCollection { get; private set; }
        public ICommand BackToMainPage { private set; get; }
        public ICommand OpenNews { get; private set; }
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool CanExecute(object parameter) {
            throw new NotImplementedException();
        }
        public void Execute(object parameter) { }
    }
}
