namespace PersonalHelper.SharedVM;

public class BaseVM : INotifyPropertyChanged
{
    protected Page CurrentPage { get => Application.Current.MainPage; }
    public ToDoVM ToDoVM { get; set; } = new ToDoVM();
    public event PropertyChangedEventHandler PropertyChanged;
    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}