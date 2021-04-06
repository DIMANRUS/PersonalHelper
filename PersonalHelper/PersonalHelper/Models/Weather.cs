using PersonalHelper.Helpers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Text.Json;

namespace PersonalHelper.Models {
    public class Weather 
    {
        public async Task<string[]> GetWheatherForOneDay()
        {
            var request = await HttpHelper.HttpRequest($"https://api.personalhelper.dimanrus.ru/api/weather/{User.GetUserCity()}");
            return request.Split("/");
        }

        /// <summary>
        /// Получение погоды на 10 дней вперёд
        /// </summary>
        /// <returns></returns>
        //public async Task<Root> GetWheatherForDailyWeek()
        //{
        //    var city = Preferences.Get("UserCity", "");

        //    var request = await httpHelper.HttpRequest($"https://api.weatherapi.com/v1/forecast.json?key=3593f8735e6a4955bc6123619210703&q={city}&days=10&aqi=no&alerts=no");

        //    Root root = JsonSerializer.Deserialize<Root>(request);

        //    return root;
        //}
    }
}