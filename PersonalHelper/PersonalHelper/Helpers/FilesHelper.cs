namespace PersonalHelper.Helpers;

static class FilesHelper
{
    private static IEnumerable<Cities> cities;

    public async static Task<IEnumerable<Cities>> GetRootJsonCities()
    {
        if (cities == null)
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("cities.json");
            using var reader = new StreamReader(stream);
            string a = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<RootJsonCities>(a).Cities;
        }
        else
            return cities;
    }
}