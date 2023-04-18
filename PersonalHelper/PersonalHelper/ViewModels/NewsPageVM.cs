namespace PersonalHelper.ViewModels;

class NewsPageVM : BaseVM
{
    private readonly News newsModel = new News();
    public NewsPageVM()
    {
        Task.Run(async () =>
        {
            NewsCityCollection = await newsModel.GetAllNewsForKeyword(User.GetUserCity());
            if (NewsCityCollection.Count != 0)
            {
                _HeightNewsCityCollection = 340;
                NotifyPropertyChanged("IsVisibleNoNewsItem");
                NotifyPropertyChanged("HeightNewsCityCollection");
            }
            NotifyPropertyChanged("NewsCityCollection");
        });
        Task.Run(async () =>
        {
            NewsCategoriesCollection = await newsModel.GetNewsCategories();
            _HeightCategoryCollection = 370 * NewsCategoriesCollection.Count;
            NotifyPropertyChanged("HeightCategoryCollection");
            NotifyPropertyChanged("NewsCategoriesCollection");
        });
        AddKeyword = new Command(execute: async () =>
        {
            if (Keyword.Length != 0)
            {
                User.AddUserNewsKeyword(Keyword);
                NewsCategoriesCollection = await newsModel.GetNewsCategories();
                _HeightCategoryCollection = 370 * NewsCategoriesCollection.Count;
                NotifyPropertyChanged("HeightCategoryCollection");
                NotifyPropertyChanged("NewsCategoriesCollection");
            }
        });
        DeleteCategory = new Command<string>(async (string keyword) =>
        {
            User.DeleteNewsKeyWord(keyword);
            NewsCategoriesCollection = await newsModel.GetNewsCategories();
            _HeightCategoryCollection = 370 * NewsCategoriesCollection.Count;
            NotifyPropertyChanged("HeightCategoryCollection");
            NotifyPropertyChanged("NewsCategoriesCollection");
        });
    }
    public NewsVM NewsVM { get; } = new NewsVM();
    public ICommand AddKeyword { get; private set; }
    public ICommand DeleteCategory { get; private set; }

    #region Private fields, property
    private int _HeightCategoryCollection = 0;
    private int _HeightNewsCityCollection = 0;
    #endregion

    #region Public properties
    public ObservableCollection<Article> NewsCityCollection { get; private set; }
    public ObservableCollection<NewsCategory> NewsCategoriesCollection { get; private set; }
    public string Keyword { get; set; }
    public int HeightCategoryCollection { get => _HeightCategoryCollection; }
    public int HeightNewsCityCollection { get => _HeightNewsCityCollection; }
    #endregion
}