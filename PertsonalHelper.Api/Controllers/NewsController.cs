using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PertsonalHelper.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase {
        HttpClient http;
        public NewsController() => http = new HttpClient();
        [HttpGet]
        public async Task<IActionResult> GetTopNews() => Ok(await http.GetStringAsync($"https://newsapi.org/v2/top-headlines?country=ru&apiKey=4045d74389e441a8b42cc9de8276ef8c"));

        [HttpGet("{city}")]
        public async Task<IActionResult> GetAllNewsForCity(string city) => Ok(await http.GetStringAsync($"https://newsapi.org/v2/everything?q={city}&apiKey=4045d74389e441a8b42cc9de8276ef8c&language=ru&pagesize=20"));
    }
}
