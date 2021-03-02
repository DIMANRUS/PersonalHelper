using Microsoft.AspNetCore.Mvc;
using PersonalHelper.Models;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace PertsonalHelper.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {

            string url = "http://api.openweathermap.org/data/2.5/weather?q=London";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response = "";

            using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = sr.ReadToEnd();
            }

            return Ok();
        }
    }
}