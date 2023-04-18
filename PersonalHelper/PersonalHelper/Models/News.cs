namespace PersonalHelper.Models;

class News
{
    private readonly ObservableCollection<NewsCategory> newsCategories;

    public News()
    {
        newsCategories = new ObservableCollection<NewsCategory>();
    }

    public async Task<ObservableCollection<Article>> GetTopNews() =>
        new(JsonSerializer.Deserialize<RootJsonNews>(await HttpHelper.HttpRequest("https://api.personalhelper.dimanrus.ru/api/news")).articles);

    public async Task<ObservableCollection<Article>> GetAllNewsForKeyword(string keyword) =>
        new(JsonSerializer.Deserialize<RootJsonNews>(await HttpHelper.HttpRequest($"https://api.personalhelper.dimanrus.ru/api/news/{keyword}")).articles);
    public async Task<ObservableCollection<NewsCategory>> GetNewsCategories()
    {
        newsCategories.Clear();
        foreach (string result in User.GetUserNewsKeyword())
        {
            if (result != "")
            {
                newsCategories.Add(new NewsCategory()
                {
                    Keyword = result,
                    ArticlesCollection = new ObservableCollection<Article>(JsonSerializer.Deserialize<RootJsonNews>(await HttpHelper.HttpRequest($"https://api.personalhelper.dimanrus.ru/api/news/{result}")).articles)
                });
            }
        }
        return newsCategories;
    }
}