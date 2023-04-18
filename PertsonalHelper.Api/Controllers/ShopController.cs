namespace PertsonalHelper.Api.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class ShopController : ControllerBase
{
    [HttpGet("{name}")]
    public async Task<List<Product>> GetProductFromDNS(string name)
    {
        List<Product> products = new List<Product>();
        Product newProduct;
        var html = @$"https://www.dns-shop.ru/search/?q={name.Replace(" ", "+")}";
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = await web.LoadFromWebAsync(html);
        var htmlBody = doc.DocumentNode.SelectSingleNode("//body");
        HtmlNodeCollection childNodes = htmlBody.ChildNodes;
        newProduct = new Product();
        foreach (var node in childNodes)
        {
            if (node.Attributes["class"]?.Value == "container category-child")
            {
                foreach (var fist in node.ChildNodes)
                {
                    if (fist.Attributes["class"]?.Value == "products-page")
                    {
                        HtmlNodeCollection one = fist.ChildNodes;
                        foreach (var result in one)
                        {
                            if (result.Attributes["class"]?.Value == "products-page__content")
                            {
                                foreach (var result2 in result.ChildNodes)
                                {
                                    if (result2.Attributes["class"]?.Value == "products-page__list")
                                    {
                                        foreach (var result3 in result2.FirstChild.FirstChild.ChildNodes)
                                        {
                                            if (result3.Attributes["class"]?.Value == "catalog-products view-simple")
                                            {
                                                foreach (var product in result3.ChildNodes)
                                                {
                                                    newProduct.Image = product.FirstChild.FirstChild.FirstChild.FirstChild.Attributes["data-srcset"].Value;
                                                    foreach (var productNodes in product.ChildNodes)
                                                    {
                                                        if (productNodes.Attributes["class"].Value == "catalog-product__name ui-link ui-link_black")
                                                        {
                                                            newProduct.Url = "https://www.dns-shop.ru/" + productNodes.Attributes["href"].Value;
                                                            newProduct.Name = productNodes.FirstChild.OuterHtml.Replace("<span>", "");
                                                        }
                                                    }
                                                    products.Add(newProduct);
                                                    newProduct = new Product();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return products;
    }
}

public class Product
{
    public string Name { get; set; }
    public string Url { get; set; }
    public string Image { get; set; }
}