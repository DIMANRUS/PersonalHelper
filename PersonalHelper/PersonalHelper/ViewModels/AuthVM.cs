using PersonalHelper.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace PersonalHelper.ViewModels {
    class AuthVM : ICommand, INotifyPropertyChanged {
        public AuthVM() {
            NextPage = new Command(execute: async () => {
                if (UserName.Length != 0 && userCityStatusChangingTextColor == Color.Green) {
                    User.SetUserCity(userCity);
                    User.SetUserName(UserName);
                    await CurrentApplication.MainPage.Navigation.PushModalAsync(new MainPage());
                } else
                    await CurrentApplication.MainPage.DisplayAlert("Ошибка", "Проверьте правильность данных", "Закрыть");
            });
            ShowLocationInformation = new Command(execute: async () => await CurrentApplication.MainPage.DisplayAlert("Зачем это нужно?", "На основе вашего введённого города будет показываться погода, новости и другая информация", "Ок"));
        }
        #region Private fiels
        private Application CurrentApplication { get => Application.Current; }
        private string userCity = "";
        private Color userCityStatusChangingTextColor = Color.Green;
        #endregion
        #region Properties
        public string UserName { get; set; }
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
        #region Interface realization
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool CanExecute(object parameter) {
            throw new NotImplementedException();
        }
        public void Execute(object parameter) {
            throw new NotImplementedException();
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}