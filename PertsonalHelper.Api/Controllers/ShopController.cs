using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PertsonalHelper.Api.Controllers {
    [ApiController]
    [Route("api/[controller]/")]
    public class ShopController : ControllerBase {
        [HttpGet("{name}")]
        public async Task<string[]> GetProductFromDNS(string name) {
            var html = @$"https://www.dns-shop.ru/search/?q={name.Replace(" ", "+")}";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = await web.LoadFromWebAsync(html);
            string[] productsText = doc.Text.Split("<div class=\"catalog-products view-simple\" data-catalog-products data-slider-available>");
            string htmltxt = productsText[1];
            string[] array = htmltxt.Split("<div class=\"pagination-container\">");
            string[] products = array[0].Split("<div data-id=\"product\" class=\"catalog-product ui-button-widget\"");
            return products;
        }
    }

    public class Product { 
        public string Name { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
    }
}