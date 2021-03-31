using PersonalHelper.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using PersonalHelper.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using PersonalHelper.SharedVM;

namespace PersonalHelper.ViewModels {
    partial class MainPageVM : BaseVM {
        public MainPageVM() {
            OpenSettings = new Command(execute: async () => {
                await CurrentPage.Navigation.PushAsync(new Settings(), true);
            });
            OpenAllToDo = new Command(execute: async () => {
                TodoItem newTodo = new TodoItem() { ItemName = "Hello", DateRemember = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToString(), TypeTodo = TypesTodo.Do };
                TodoItemDatabase todoItemDatabase = await TodoItemDatabase.Instance;
                await todoItemDatabase.SaveItemAsync(newTodo);
                IEnumerable<TodoItem> listtodo = await todoItemDatabase.GetItemsTodayAsync();
                IEnumerable<TodoItem> listAllToDo = await todoItemDatabase.GetAllToDo();
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
            CompleteTask = new Command<int>(async (int todoId) => { });
            RemoveTask = new Command<int>(async (int todoId) => { });
            AddTask = new Command<int>(async (int todoId) => { });
        }
        #region ToDo
        private ObservableCollection<TodoItem> todoItemsToday;
        public ObservableCollection<TodoItem> TodoItemsToday { get => todoItemsToday; }
        public ICommand CompleteTask { get; private set; }
        public ICommand RemoveTask { get; private set; }
        public ICommand AddTask { get; private set; }
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
    }
}