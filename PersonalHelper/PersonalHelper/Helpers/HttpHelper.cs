using System.Net.Http;
using System.Threading.Tasks;

namespace PersonalHelper.Helpers 
{
    class HttpHelper
    {
        public async Task<string> HttpRequest(string url) => await (new HttpClient()).GetStringAsync(url);         
    }
}