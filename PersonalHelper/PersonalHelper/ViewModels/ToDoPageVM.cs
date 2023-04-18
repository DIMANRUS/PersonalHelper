namespace PersonalHelper.ViewModels;
class ToDoPageVM : BaseVM
{
    public ToDoPageVM()
    {
        Task.Run(async () =>
        {
            TodoItemDatabase db = await TodoItemDatabase.Instance;
            ToDoCompleteToday = new ObservableCollection<TodoItem>(await db.GetToDoCompleteTodayAsync());
            NotifyPropertyChanged(nameof(ToDoCompleteToday));
            ToDoTomorrow = new ObservableCollection<TodoItem>(await db.GetToDoTomorrowAsync());
            NotifyPropertyChanged(nameof(ToDoTomorrow));
            ToDoAll = new ObservableCollection<TodoItem>(await db.GetAllToDo());
            NotifyPropertyChanged(nameof(ToDoAll));
        });
        ToDoVM.RemoveTask = new Command<int>(async (int todoId) =>
        {
            TodoItemDatabase db = await TodoItemDatabase.Instance;
            await db.RemoveTaskAsync(todoId);
            ToDoCompleteToday = new ObservableCollection<TodoItem>(await db.GetToDoCompleteTodayAsync());
            NotifyPropertyChanged(nameof(ToDoCompleteToday));
            ToDoTomorrow = new ObservableCollection<TodoItem>(await db.GetToDoTomorrowAsync());
            NotifyPropertyChanged(nameof(ToDoTomorrow));
            ToDoAll = new ObservableCollection<TodoItem>(await db.GetAllToDo());
            NotifyPropertyChanged(nameof(ToDoAll));
        });
    }
    public ObservableCollection<TodoItem> ToDoCompleteToday { get; set; }
    public ObservableCollection<TodoItem> ToDoTomorrow { get; set; }
    public ObservableCollection<TodoItem> ToDoAll { get; set; }
    public DateTime NextDay { get => DateTime.Now.AddDays(1); }
}