namespace PersonalHelper.Models;

class NewsCategory
{
    public string Keyword { get; set; }
    public ObservableCollection<Article> ArticlesCollection { get; set; }
}