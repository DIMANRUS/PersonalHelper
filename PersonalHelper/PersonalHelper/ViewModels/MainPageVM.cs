using PersonalHelper.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PersonalHelper.Models;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;

namespace PersonalHelper.ViewModels {
    partial class MainPageVM : INotifyPropertyChanged {
        public MainPageVM() {
            OpenSettings = new Command(execute: async () => {
                await CurrentPage.Navigation.PushAsync(new Settings(), true);
            });
            OpenAllToDo = new Command(execute: async () => {
                TodoItem newTodo = new TodoItem() { ItemName = "Hello", DateRemember = DateTime.Now };
                TodoItemDatabase todoItemDatabase = await TodoItemDatabase.Instance;
                await todoItemDatabase.SaveItemAsync(newTodo);
                IEnumerable<TodoItem> listtodo = await todoItemDatabase.GetItemsTodayAsync();
                todoItemsToday = new ObservableCollection<TodoItem>(listtodo);
                NotifyPropertyChanged(nameof(TodoItemsToday));
            });
            OpenAllNews = new Command(execute: async () => {
                await CurrentPage.Navigation.PushAsync(new NewsPage());
            });
            OpenWeatherForOneDay = new Command(execute: async () => {
                await CurrentPage.Navigation.PushAsync(new WeatherPage());
            });
            Task.Run(async () => {
                TodoItemDatabase db = await TodoItemDatabase.Instance;
                todoItemsToday = new ObservableCollection<TodoItem>(await db.GetItemsTodayAsync());
                NotifyPropertyChanged(nameof(TodoItemsToday));
                NewsCollection = await newsModel.GetTopNews();
                NotifyPropertyChanged(nameof(NewsCollection));
            });
        }
        #region ToDo
        private ObservableCollection<TodoItem> todoItemsToday;
        public ObservableCollection<TodoItem> TodoItemsToday { get => todoItemsToday; }
        #endregion
        private Wheather wheather { get; set; }
        public ICommand OpenWeatherForOneDay { get; set; }
        public ICommand OpenWeatherForWeek { get; private set; }
        private readonly News newsModel = new News();
        public ObservableCollection<Article> NewsCollection { get; private set; }
        public ICommand OpenAllNews { get; private set; }
        private Page CurrentPage { get => Application.Current.MainPage; }
        public ICommand OpenSettings { private set; get; }
        public ICommand OpenAllToDo { get; private set; }
        public string HelloUserName { get => "Доброе утро, " + User.GetUserName(); }
        public NewsVM NewsVM { get; } = new NewsVM();
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}