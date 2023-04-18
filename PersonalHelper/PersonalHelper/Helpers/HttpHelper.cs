namespace PersonalHelper.Helpers;
static class HttpHelper
{
    private static readonly HttpClient httpClient = new();
    public static async Task<string> HttpRequest(string url) => await httpClient.GetStringAsync(url);
}