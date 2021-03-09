using PersonalHelper.Helpers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Text.Json;

namespace PersonalHelper.Models {
    public class Wheather 
    {
        private readonly HttpHelper httpHelper;

        public Wheather()
        {
            httpHelper = new HttpHelper();
        }

        /// <summary>
        /// Получение погоды на текущий день
        /// </summary>
        /// <returns></returns>
        public async Task<Current> GetWheatherForOneDay()
        {
            var city = Preferences.Get("UserCity", "");

            var request = await httpHelper.HttpRequest($"https://api.weatherapi.com/v1/current.json?key=3593f8735e6a4955bc6123619210703&q={city}&aqi=no");

            Current current = JsonSerializer.Deserialize<Current>(request);

            return current;
        }

        /// <summary>
        /// Получение погоды на 10 дней вперёд
        /// </summary>
        /// <returns></returns>
        public async Task<Root> GetWheatherForDailyWeek()
        {
            var city = Preferences.Get("UserCity", "");

            var request = await httpHelper.HttpRequest($"https://api.weatherapi.com/v1/forecast.json?key=3593f8735e6a4955bc6123619210703&q={city}&days=10&aqi=no&alerts=no");

            Root root = JsonSerializer.Deserialize<Root>(request);

            return root;
        }
    }
}