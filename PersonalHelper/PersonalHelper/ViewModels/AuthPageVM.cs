using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PersonalHelper.Helpers;
using PersonalHelper.Models;
using PersonalHelper.SharedVM;
using Xamarin.Forms;

namespace PersonalHelper.ViewModels {
    class AuthPageVM : BaseVM {
        public AuthPageVM() {
            NextPage = new Command(async () => {
                if (userName != "" && userCityStatusChangingTextColor == Color.Green && User.GetUserZodiakId() != "null") {
                    User.SetUserCity(userCity);
                    User.SetUserName(userName);
                    await CurrentApplication.MainPage.Navigation.PushAsync(new MainPage());
                } else
                    await CurrentApplication.MainPage.DisplayAlert("Ошибка", "Проверьте правильность данных", "Закрыть");
            });
            ShowLocationInformation = new Command(execute: async () => await CurrentApplication.MainPage.DisplayAlert("Зачем это нужно?", "На основе вашего введённого города будет показываться погода, новости и другая информация", "Ок"));
            ChangedCity = new Command<string>(async (string NameCity) => {
                userCity = NameCity.Replace(" ", "").ToLower();
                if (await User.VerifyCity(userCity)) {
                    userCityStatusChangingTextColor = Color.Green;
                    NotifyPropertyChanged(nameof(UserCityStatusChangingTextColor));
                } else {
                    userCityStatusChangingTextColor = Color.Red;
                    NotifyPropertyChanged(nameof(UserCityStatusChangingTextColor));
                }
            });
            ChangedZodiak = new Command<int>((int zodiakId) =>
                 User.SetUserZodiak(zodiakId.ToString()));
        }
        #region Private fiels
        private Application CurrentApplication { get => Application.Current; }
        private string userCity = "";
        private Color userCityStatusChangingTextColor = Color.Red;
        private string userName = "";
        #endregion
        #region Properties
        public ICommand ChangedZodiak { get; private set; }
        public string UserName { get => userName; set => userName = value; }
        public Command ChangedCity { get; set; }
        public Color UserCityStatusChangingTextColor { get => userCityStatusChangingTextColor; }
        #endregion
        #region Commands
        public Command NextPage { get; private set; }
        public Command ShowLocationInformation { get; private set; }
        #endregion
    }
}