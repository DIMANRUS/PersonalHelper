using PersonalHelper.Views;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using PersonalHelper.Models;
using System.Collections.ObjectModel;
using PersonalHelper.SharedVM;
using System;
using System.Collections.Generic;
using PersonalHelper.Interfaces;

namespace PersonalHelper.ViewModels {
    partial class MainPageVM : BaseVM {
        public MainPageVM() {
            OpenSettings = new Command(async () => {
                await CurrentPage.Navigation.PushAsync(new Settings(), true);
            });
            OpenAllToDo = new Command(async () => await CurrentPage.Navigation.PushAsync(new ToDoPage()));
            OpenAllNews = new Command(async () => {
                await CurrentPage.Navigation.PushAsync(new NewsPage());
            });
            OpenWeatherForOneDay = new Command(async () => {
                await CurrentPage.Navigation.PushAsync(new WeatherPage());
            });
            Task.Run(async () => {
                db = await TodoItemDatabase.Instance;
                todoItemsToday = new ObservableCollection<TodoItem>(await db.GetItemsTodayAsync());
                NewsCollection = await newsModel.GetTopNews();
                NotifyPropertyChanged(nameof(TodoItemsToday));
                NotifyPropertyChanged(nameof(NewsCollection));
            });
            ToDoVM.CompleteTask = new Command<int>(async (int todoId) => {
                await db.CompleteTaskAsync(todoId); 
                todoItemsToday = new ObservableCollection<TodoItem>(await db.GetItemsTodayAsync()); 
                NotifyPropertyChanged(nameof(TodoItemsToday)); });
            ToDoVM.RemoveTask = new Command<int>(async (int todoId) => {
                await db.RemoveTaskAsync(todoId);
                todoItemsToday = new ObservableCollection<TodoItem>(await db.GetItemsTodayAsync());
                NotifyPropertyChanged(nameof(TodoItemsToday));
            });
            AddTask = new Command<string>(async(string taskText) => {
                taskDate = taskDate.Date + TaskTimePicker;
                TodoItem newToDo = new TodoItem() { 
                    ItemName = taskText,
                    DateRemember = taskDate,
                    TypeTodo = TypesTodo.Do
                };
                await db.SaveItemAsync(newToDo);
                DependencyService.Get<INotificationManager>().SendNotification("Пора выполнить задачу!", newToDo.ItemName, newToDo.DateRemember);
                todoItemsToday = new ObservableCollection<TodoItem>(await db.GetItemsTodayAsync());
                NotifyPropertyChanged(nameof(TodoItemsToday));
            });
            TaskDateCommand = new Command<DatePicker>((DatePicker datePicker) => taskDate = datePicker.Date);
        }
        #region Notifycation
        #endregion
        private static TodoItemDatabase db;
        #region ToDo
        private ObservableCollection<TodoItem> todoItemsToday;
        public ObservableCollection<TodoItem> TodoItemsToday { get => todoItemsToday; }
        #region Add New Task
        public ICommand AddTask { get; private set; }
        public ICommand TaskDateCommand { get; private set; }
        private DateTime taskDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public TimeSpan TaskTimePicker { get; set; }
        #endregion
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