using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PertsonalHelper.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class HoroscopeController : ControllerBase {
        [HttpGet("{id}")]
        public async Task<string> GetHoroscop(int id) {
            string zodiak = id switch {
                0 => "scorpio",
                1 => "aries",
                2 => "sagittarius",
                3 => "leo",
                4 => "virgo",
                5 => "libra",
                6 => "capricorn",
                7 => "aquarius",
                8 => "pisces",
                9 => "cancer",
                10 => "gemini",
                11 => "taurus"
            };
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = await web.LoadFromWebAsync($"https://horo.mail.ru/prediction/{zodiak}/today");
            string[] productsText = doc.Text.Split("<div class=\"article__item article__item_alignment_left article__item_html\">");
            string htmltxt = productsText[1].Split("</div>")[0].Replace("<p>", " ").Replace("</p>", " ");
            return htmltxt;
        }
    }
}