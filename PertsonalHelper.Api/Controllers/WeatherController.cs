using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
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

            using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = await sr.ReadToEndAsync();
            }

            var values = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response);

            var main = values.FirstOrDefault(x => x.Key == "main").Value.temp;

            return Ok(response);
        }
    }
}