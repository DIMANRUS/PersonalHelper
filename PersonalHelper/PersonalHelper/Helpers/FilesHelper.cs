using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using PersonalHelper.Models;
using Xamarin.Essentials;

namespace PersonalHelper.Helpers {
    static class FilesHelper {
        private static Stream fileStream;
        public async static Task<RootJsonCities> GetRootJsonCities() {
            using (var stream = await FileSystem.OpenAppPackageFileAsync("cities.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    return JsonSerializer.Deserialize<RootJsonCities>(await reader.ReadToEndAsync());
                }
            }
        }
    }
}