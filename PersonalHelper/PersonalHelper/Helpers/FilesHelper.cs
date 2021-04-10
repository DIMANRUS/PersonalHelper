using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using PersonalHelper.Models;
using Xamarin.Essentials;

namespace PersonalHelper.Helpers {
    static class FilesHelper {
        private static IEnumerable<Cities> cities = null;
        public async static Task<IEnumerable<Cities>> GetRootJsonCities() {
            if (cities == null) {
                using (var stream = await FileSystem.OpenAppPackageFileAsync("cities.json")) {
                    using (var reader = new StreamReader(stream)) {
                        string a = await reader.ReadToEndAsync();
                        return JsonSerializer.Deserialize<RootJsonCities>(a).Cities;
                    }
                }
            } else
                return cities;
        }
    }
}