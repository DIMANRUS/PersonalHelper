using System.Net.Http;
using System.Threading.Tasks;

namespace PersonalHelper.Helpers 
{
    static class HttpHelper
    {
        private static HttpClient httpClient = new HttpClient();
        public static async Task<string> HttpRequest(string url)=> await httpClient.GetStringAsync(url);
    }
}