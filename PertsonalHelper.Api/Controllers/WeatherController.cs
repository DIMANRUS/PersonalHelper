using Microsoft.AspNetCore.Mvc;
using PersonalHelper.Api.Models;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace PertsonalHelper.Api.Controllers {
    [ApiController]
    [Route("api/[controller]/")]
    public class WeatherController : ControllerBase {
        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city) {
            HttpClient http = new HttpClient();
            string json = await http.GetStringAsync($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid=96c2dd40e5856b4f9eb6af0be8ef3c7d&units=metric");
            RootJsonWheather result = JsonSerializer.Deserialize<RootJsonWheather>(json);
            return Ok(result.main.temp + @"/" + $"https://openweathermap.org/img/wn/{result.weather.First().icon}@2x.png".Replace("/", "'"));
        }

        [HttpGet("Detail/{city}")]
        public async Task<IActionResult> GetWeatherForDetailDay(string city) {
            HttpClient http = new HttpClient();
            return Ok(await http.GetStringAsync($"https://api.weatherapi.com/v1/forecast.json?key=3593f8735e6a4955bc6123619210703&q={city}&days=10&aqi=no&alerts=no"));
        }
    }
}