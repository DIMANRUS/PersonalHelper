using PersonalHelper.Models;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonalHelper.ViewModels
{
    partial class MainPageVM : INotifyPropertyChanged
    {
        private Wheather wheather { get; set; }

        public ICommand OpenWeatherForOneDay { get; set; }

        public ICommand OpenWeatherForWeek { get; private set; }
    }
}