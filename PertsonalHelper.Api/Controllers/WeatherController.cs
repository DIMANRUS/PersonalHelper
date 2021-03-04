using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalHelper.Api.Models;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PertsonalHelper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class WeatherController : ControllerBase
    {
        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            // сервер получение погоды 
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid=7cfd697d8dcb30e786d31dd24802a29d&units=metric";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response = "";

            using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream())) {
                response = await sr.ReadToEndAsync();
            }

            RootJsonWheather result = JsonConvert.DeserializeObject<RootJsonWheather>(response);

            return Ok(result.weather.First().description + @"/" + result.main.temp);


            //HttpClient http = new HttpClient();
            //string json = await http.GetStringAsync($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid=7cfd697d8dcb30e786d31dd24802a29d&units=metric");
            //RootJsonWheather result = JsonConvert.DeserializeObject<RootJsonWheather>(json);
            //return Ok(result.weather.First().description + @"/" + result.main.temp);
        }
    }
}