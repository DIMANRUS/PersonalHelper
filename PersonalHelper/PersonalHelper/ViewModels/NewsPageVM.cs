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
    class NewsPageVM : INotifyPropertyChanged {
        private readonly News newsModel = new News();
        public NewsPageVM() {
            Task.Run(async () => {
                NewsCityCollection = await newsModel.GetAllNewsForKeyword(User.GetUserCity());
                if (NewsCityCollection.Count != 0)
                {
                    _IsVisibleNoNewsIcon = "false";
                    _HeightNewsCityCollection = 330;
                    NotifyPropertyChanged("IsVisibleNoNewsItem");
                    NotifyPropertyChanged("HeightNewsCityCollection");
                }
                NotifyPropertyChanged("NewsCityCollection");
            });
            Task.Run(async () => {
                NewsCategoriesCollection = await newsModel.GetNewsCategories();
                _HeightCategoryCollection = 370 * NewsCategoriesCollection.Count;
                NotifyPropertyChanged("HeightCategoryCollection");
                NotifyPropertyChanged("NewsCategoriesCollection");
            });
            AddKeyword = new Command(execute: async () => {
                if (Keyword.Length != 0) {
                    User.AddUserNewsKeyword(Keyword);
                    NewsCategoriesCollection = await newsModel.GetNewsCategories();
                    _HeightCategoryCollection = 370 * NewsCategoriesCollection.Count;
                    NotifyPropertyChanged("HeightCategoryCollection");
                    NotifyPropertyChanged("NewsCategoriesCollection");
                }
            });
            DeleteCategory = new Command<string>(async(string keyword) => {
                User.DeleteNewsKeyWord(keyword);
                NewsCategoriesCollection = await newsModel.GetNewsCategories();
                _HeightCategoryCollection = 370 * NewsCategoriesCollection.Count;
                NotifyPropertyChanged("HeightCategoryCollection");
                NotifyPropertyChanged("NewsCategoriesCollection");
            });
        }
        #region Private fields, property
        private Page CurrentPage { get => Application.Current.MainPage; }
        private int _HeightCategoryCollection = 0;
        private int _HeightNewsCityCollection = 0;
        private string _IsVisibleNoNewsIcon = "true";
        #endregion
        #region Public properties
        public ObservableCollection<Article> NewsCityCollection { get; private set; }
        public ObservableCollection<NewsCategory> NewsCategoriesCollection { get; private set; }
        public string Keyword { get; set; }
        public int HeightCategoryCollection { get => _HeightCategoryCollection; }
        public NewsVM NewsVM { get; } = new NewsVM();
        public string IsVisibleNoNewsItem { get => _IsVisibleNoNewsIcon; }
        public int HeightNewsCityCollection { get => _HeightNewsCityCollection; }
        #endregion
        #region Commands
        public ICommand AddKeyword { get; private set; }
        public ICommand DeleteCategory { get; private set; }
        #endregion
        #region Interface realization
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}