using PersonalHelper.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PersonalHelper.ViewModels {
    class NewsPageVM : ICommand, INotifyPropertyChanged {
        private readonly News newsModel = new News();
        public NewsPageVM() {
            BackToMainPage = new Command(execute: async () => { await CurrentPage.Navigation.PopModalAsync(); });
            Task.Run(async () => {
                NewsCityCollection = await newsModel.GetAllNewsForKeyword(User.GetUserCity());
                NotifyPropertyChanged("NewsCityCollection");
            });
            Task.Run(async () => {
                NewsCategoriesCollection = await newsModel.GetNewsCategories();
                NotifyPropertyChanged("NewsCategoriesCollection");
            });
            OpenNews = new Command<string>(execute: async (string url) => {
                await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
            });
            AddKeyword = new Command(execute: async () => {
                if (Keyword.Length != 0) {
                    User.AddUserNewsKeyword(Keyword);
                    NewsCategoriesCollection = await newsModel.GetNewsCategories();
                    NotifyPropertyChanged("NewsCategoriesCollection");
                }
            });
        }
        #region Private fields, property
        private Page CurrentPage { get => Application.Current.MainPage; }
        #endregion
        #region Public properties
        public ObservableCollection<Article> NewsCityCollection { get; private set; }
        public ObservableCollection<NewsCategory> NewsCategoriesCollection { get; private set; }
        public string Keyword { get; set; }
        #endregion
        #region Commands
        public ICommand BackToMainPage { private set; get; }
        public ICommand OpenNews { get; private set; }
        public ICommand AddKeyword { get; private set; }
        #endregion
        #region Interface realization
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool CanExecute(object parameter) {
            throw new NotImplementedException();
        }
        public void Execute(object parameter) { }
        #endregion
    }
}