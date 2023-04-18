namespace PersonalHelper.Models;

public class Weather
{
    public async Task<string[]> GetWheatherForOneDay()
    {
        string request = await HttpHelper.HttpRequest($"https://api.personalhelper.dimanrus.ru/api/weather/{User.GetUserCity()}");
        string[] arr = request.Split("/");
        arr[1] = arr[1].Replace("'", "/");
        return arr;
    }

    public async Task<ObservableCollection<Hour>> GetWheatherForDetailDay()
    {
        var request = await HttpHelper.HttpRequest($"https://api.personalhelper.dimanrus.ru/api/weather/detail/{User.GetUserCity()}");
        return new ObservableCollection<Hour>(JsonSerializer.Deserialize<RootJsonWeather>(request).forecast.forecastday.First().hour);
    }
}