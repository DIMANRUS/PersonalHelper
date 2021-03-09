using System.IO;
using System.Reflection;
using System.Text.Json;
using PersonalHelper.Models;

namespace PersonalHelper.Helpers {
    static class FilesHelper {
        private static Stream fileStream;
        public static RootJsonCities GetRootJsonCities() {
            fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PersonalHelper.Files.cities.json");
            using (var reader = new StreamReader(fileStream)) {
                return JsonSerializer.Deserialize<RootJsonCities>(reader.ReadToEnd());
            }
        }
    }
}