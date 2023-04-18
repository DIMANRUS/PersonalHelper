namespace PersonalHelper;

public static class Constants
{
    public const string DatabaseFilename = "TodoList.db3";
    public const SQLiteOpenFlags Flags =
        SQLiteOpenFlags.ReadWrite |
        SQLiteOpenFlags.Create |
        SQLiteOpenFlags.SharedCache;
    public static string DatabasePath
    {
        get
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(basePath, DatabaseFilename);
        }
    }
}
public class TodoItemDatabase
{
    static SQLiteAsyncConnection Database;
    public static readonly AsyncLazy<TodoItemDatabase> Instance = new(async () =>
    {
        var instance = new TodoItemDatabase();
        CreateTableResult result = await Database.CreateTableAsync<TodoItem>();
        return instance;
    });
    public TodoItemDatabase()
    {
        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
    }
    public class AsyncLazy<T> : Lazy<Task<T>>
    {
        private readonly Lazy<Task<T>> instance;
        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public TaskAwaiter<T> GetAwaiter() => instance.Value.GetAwaiter();
    }

    #region Methods for job with DataBase
    public async Task<IEnumerable<TodoItem>> GetAllToDo() => await Database.Table<TodoItem>().ToListAsync();
    public async Task DeleteAllItems() => await Database.DeleteAllAsync<TodoItem>();
    public async Task<IEnumerable<TodoItem>> GetItemsTodayAsync() => (await Database.Table<TodoItem>().ToListAsync()).Where(x => x.DateRemember.Date == DateTime.Now.Date && x.TypeTodo == TypesTodo.Do);
    public async Task<IEnumerable<TodoItem>> GetToDoCompleteTodayAsync() => (await Database.Table<TodoItem>().ToListAsync()).Where(x => x.DateRemember.Date == DateTime.Now.Date && x.TypeTodo == TypesTodo.Complete);
    public async Task<IEnumerable<TodoItem>> GetToDoTomorrowAsync() => (await Database.Table<TodoItem>().ToListAsync()).Where(x => x.DateRemember.Date == DateTime.Now.AddDays(1).Date);
    public async Task CompleteTaskAsync(int taskId)
    {
        TodoItem todoItem = await Database.Table<TodoItem>().FirstOrDefaultAsync(x => x.Id == taskId);
        todoItem.TypeTodo = TypesTodo.Complete;
        await Database.UpdateAsync(todoItem);
    }
    public async Task RemoveTaskAsync(int taskId)
    {
        TodoItem todoItem = await Database.Table<TodoItem>().FirstOrDefaultAsync(x => x.Id == taskId);
        await Database.DeleteAsync(todoItem);
    }
    public Task<int> SaveItemAsync(TodoItem item) => Database.InsertAsync(item);
    #endregion
}