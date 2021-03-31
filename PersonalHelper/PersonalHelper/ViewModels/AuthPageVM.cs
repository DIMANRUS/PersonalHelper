using PersonalHelper.Models;
using PersonalHelper.SharedVM;
using Xamarin.Forms;

namespace PersonalHelper.ViewModels {
    class AuthPageVM : BaseVM {
        public AuthPageVM() {
            NextPage = new Command(execute: async () => {
                if (userName != "" && userCityStatusChangingTextColor == Color.Green) {
                    User.SetUserCity(userCity);
                    User.SetUserName(userName);
                    await CurrentApplication.MainPage.Navigation.PushAsync(new MainPage());
                } else
                    await CurrentApplication.MainPage.DisplayAlert("Ошибка", "Проверьте правильность данных", "Закрыть");
            });
            ShowLocationInformation = new Command(execute: async () => await CurrentApplication.MainPage.DisplayAlert("Зачем это нужно?", "На основе вашего введённого города будет показываться погода, новости и другая информация", "Ок"));
        }
        #region Private fiels
        private Application CurrentApplication { get => Application.Current; }
        private string userCity = "";
        private Color userCityStatusChangingTextColor = Color.Red;
        private string userName = "";
        #endregion
        #region Properties
        public string UserName { get => userName; set => userName = value; }
        public string UserCity {
            get => userCity; set {
                userCity = value;
                if (User.VerifyCity(value)) {
                    userCityStatusChangingTextColor = Color.Green;
                    NotifyPropertyChanged("UserCityStatusChangingTextColor");
                } else {
                    userCityStatusChangingTextColor = Color.Red;
                    NotifyPropertyChanged("UserCityStatusChangingTextColor");
                }
            }
        }
        public Color UserCityStatusChangingTextColor { get => userCityStatusChangingTextColor; }
        #endregion
        #region Commands
        public Command NextPage { get; private set; }
        public Command ShowLocationInformation { get; private set; }
        #endregion
    }
}