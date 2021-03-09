using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using PersonalHelper.Helpers;

namespace PersonalHelper.Models {
    class News {
        HttpHelper httpHelper = new HttpHelper();
        private ObservableCollection<NewsCategory> newsCategories;
        public News() {
            newsCategories = new ObservableCollection<NewsCategory>();
        }
        public async Task<ObservableCollection<Article>> GetTopNews() =>
            new ObservableCollection<Article>(JsonSerializer.Deserialize<RootJsonNews>(await httpHelper.HttpRequest("https://api.personalhelper.dimanrus.ru/api/news")).articles);
        //https://newsapi.org/v2/everything?q=%D0%AF%D0%BA%D1%83%D1%82%D1%81%D0%BA&apiKey=4045d74389e441a8b42cc9de8276ef8c&language=ru&pagesize=5
        public async Task<ObservableCollection<Article>> GetAllNewsForKeyword(string keyword) =>
            new ObservableCollection<Article>(JsonSerializer.Deserialize<RootJsonNews>(await httpHelper.HttpRequest($"https://api.personalhelper.dimanrus.ru/api/news/{keyword}")).articles);
        public async Task<ObservableCollection<NewsCategory>> GetNewsCategories() {
            newsCategories.Clear();
            foreach (string result in User.GetUserNewsKeyword()) {
                if (result != "") {
                    newsCategories.Add(new NewsCategory() {
                        Keyword = result,
                        ArticlesCollection = new ObservableCollection<Article>(JsonSerializer.Deserialize<RootJsonNews>(await httpHelper.HttpRequest($"https://api.personalhelper.dimanrus.ru/api/news/{result}")).articles)
                    });
                }
            }
            return newsCategories;
        }
    }
}