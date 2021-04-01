using PersonalHelper.Views;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using PersonalHelper.Models;
using System.Collections.ObjectModel;
using PersonalHelper.SharedVM;

namespace PersonalHelper.ViewModels {
    partial class MainPageVM : BaseVM {
        public MainPageVM() {
            OpenSettings = new Command(execute: async () => {
                await CurrentPage.Navigation.PushAsync(new Settings(), true);
            });
            //OpenAllToDo = new Command(execute: async () => {
            //});
            OpenAllNews = new Command(execute: async () => {
                await CurrentPage.Navigation.PushAsync(new NewsPage());
            });
            OpenWeatherForOneDay = new Command(execute: async () => {
                await CurrentPage.Navigation.PushAsync(new WeatherPage());
            });
            Task.Run(async () => {
                db = await TodoItemDatabase.Instance;
                todoItemsToday = new ObservableCollection<TodoItem>(await db.GetItemsTodayAsync());
                NewsCollection = await newsModel.GetTopNews();
                NotifyPropertyChanged(nameof(TodoItemsToday));
                NotifyPropertyChanged(nameof(NewsCollection));
            });
            CompleteTask = new Command<int>(async (int todoId) => { await db.CompleteTaskAsync(todoId); 
                todoItemsToday = new ObservableCollection<TodoItem>(await db.GetItemsTodayAsync()); 
                NotifyPropertyChanged(nameof(TodoItemsToday)); });
            RemoveTask = new Command<int>(async (int todoId) => {
                await db.RemoveTaskAsync(todoId);
                todoItemsToday = new ObservableCollection<TodoItem>(await db.GetItemsTodayAsync());
                NotifyPropertyChanged(nameof(TodoItemsToday));
            });
            //AddTask = new Command<int>(async (int todoId) => { });
        }
        private static TodoItemDatabase db;
        #region ToDo

        private ObservableCollection<TodoItem> todoItemsToday;
        public ObservableCollection<TodoItem> TodoItemsToday { get => todoItemsToday; }
        public ICommand CompleteTask { get; private set; }
        public ICommand RemoveTask { get; private set; } = new Command<int>(async (int taskId) => await db.RemoveTaskAsync(taskId));
        public ICommand AddTask { get; private set; }
        #endregion
        private Wheather wheather { get; set; }
        public ICommand OpenWeatherForOneDay { get; set; }
        public ICommand OpenWeatherForWeek { get; private set; }
        private readonly News newsModel = new News();
        public ObservableCollection<Article> NewsCollection { get; private set; }
        public ICommand OpenAllNews { get; private set; }
        public ICommand OpenSettings { private set; get; }
        public ICommand OpenAllToDo { get; private set; }
        public string HelloUserName { get => "Доброе утро, " + User.GetUserName(); }
        public NewsVM NewsVM { get; } = new NewsVM();
    }
}