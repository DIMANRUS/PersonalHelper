namespace PersonalHelper.ViewModels;

public class NewsVM
{
    public NewsVM()
    {
        OpenNews = new Command<string>(execute: async (string url) =>
        {
            await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
        });
    }
    public ICommand OpenNews { get; private set; }
}