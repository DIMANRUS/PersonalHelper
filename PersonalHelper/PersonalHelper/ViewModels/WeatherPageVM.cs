namespace PersonalHelper.ViewModels;

class WeatherPageVM : BaseVM
{
    public WeatherPageVM()
    {
        Task.Run(async () =>
        {
            Weather weather = new();
            DetailDayWeather = await weather.GetWheatherForDetailDay();
            foreach (Hour result in DetailDayWeather)
            {
                result.time = result.time.Substring(result.time.Length - 5);
                result.condition.icon = $"https:{result.condition.icon}";
            }
            NotifyPropertyChanged(nameof(DetailDayWeather));
        });
    }
    public ObservableCollection<Hour> DetailDayWeather { get; private set; }
}