using PersonalHelper.Views;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using PersonalHelper.Models;
using System.Collections.ObjectModel;
using PersonalHelper.SharedVM;
using System;
using PersonalHelper.Interfaces;
using PersonalHelper.Helpers;

namespace PersonalHelper.ViewModels {
    partial class MainPageVM : BaseVM {
        public MainPageVM() {
            Task.Run(async () =>
            await Initializate());
            ZnakZodiaka = User.GetUserZodiakString();
            NotifyPropertyChanged(nameof(ZnakZodiaka));
            OpenSettings = new Command(async () =>
            {
                await CurrentPage.Navigation.PushAsync(new Settings(), true);
            });
            OpenAllToDo = new Command(async () => await CurrentPage.Navigation.PushAsync(new ToDoPage()));
            OpenAllNews = new Command(async () =>
            {
                await CurrentPage.Navigation.PushAsync(new NewsPage());
            });
            OpenAllWeather = new Command(async () =>
            {
                await CurrentPage.Navigation.PushAsync(new WeatherPage());
            });
            ToDoVM.CompleteTask = new Command<int>(async (int todoId) =>
            {
                await db.CompleteTaskAsync(todoId);
                todoItemsToday = new ObservableCollection<TodoItem>(await db.GetItemsTodayAsync());
                NotifyPropertyChanged(nameof(TodoItemsToday));
            });
            ToDoVM.RemoveTask = new Command<int>(async (int todoId) =>
            {
                await db.RemoveTaskAsync(todoId);
                todoItemsToday = new ObservableCollection<TodoItem>(await db.GetItemsTodayAsync());
                NotifyPropertyChanged(nameof(TodoItemsToday));
            });
            AddTask = new Command<string>(async (string taskText) =>
            {
                taskDate = taskDate.Date + TaskTimePicker;
                TodoItem newToDo = new TodoItem()
                {
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
        private async Task Initializate()
        {
            db = await TodoItemDatabase.Instance;
            todoItemsToday = new ObservableCollection<TodoItem>(await db.GetItemsTodayAsync());
            NewsCollection = await newsModel.GetTopNews();
            string[] weatherFromApi = await weatherModel.GetWheatherForOneDay();
            Temperature = weatherFromApi[0];
            IconSource = weatherFromApi[1];
            Horoscope = await HttpHelper.HttpRequest($"https://api.personalhelper.dimanrus.ru/api/horoscope/{User.GetUserZodiakId()}");
            NotifyPropertyChanged(nameof(TodoItemsToday));
            NotifyPropertyChanged(nameof(NewsCollection));
            NotifyPropertyChanged(nameof(Temperature));
            NotifyPropertyChanged(nameof(IconSource));
            NotifyPropertyChanged(nameof(Horoscope));
        }
        private TodoItemDatabase db;
        private readonly News newsModel = new News();
        private readonly Weather weatherModel = new Weather();
        public NewsVM NewsVM { get; } = new NewsVM();
        #region Notifycation
        #endregion
        #region ToDo
        private ObservableCollection<TodoItem> todoItemsToday;
        public ObservableCollection<TodoItem> TodoItemsToday { get => todoItemsToday; }
        public ICommand OpenAllToDo { get; private set; }
        #region Add New Task
        public ICommand AddTask { get; private set; }
        public ICommand TaskDateCommand { get; private set; }
        private DateTime taskDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public TimeSpan TaskTimePicker { get; set; }
        #endregion
        #endregion
        #region Weather
        public string Temperature { get; private set; }
        public string IconSource { get; private set; }
        public ICommand OpenAllWeather { get; private set; }
        public string UserCity { get => User.GetUserCity(); }
        #endregion
        #region News
        public ObservableCollection<Article> NewsCollection { get; private set; }
        public ICommand OpenAllNews { get; private set; }
        #endregion
        #region Horoscope
        public string Horoscope { get; private set; }
        public string ZnakZodiaka { get; private set; }
        #endregion
        public ICommand OpenSettings { private set; get; }
        public string HelloUserName { get => "Доброе утро, " + User.GetUserName(); }
    }
}