using PersonalHelper.Models;
using PersonalHelper.Views;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PersonalHelper.ViewModels {
    class SettingsVm : ICommand, INotifyPropertyChanged {
        #region Commands realization
        public SettingsVm() {
            BackToMainPage = new Command(execute: async () => { await CurrentPage.Navigation.PopAsync(); });
            Exit = new Command(execute: async()=> {
                User.ClearUserData();
                await CurrentPage.Navigation.PushModalAsync(new Auth(), true);
            });
        }
        #endregion
        private Page CurrentPage { get => Application.Current.MainPage; }
        #region Private field
        private string userCity = User.GetUserCity();
        private Color userCityStatusChangingTextColor = Color.Green;
        #endregion
        #region Properties
        public string UserName { get => User.GetUserName(); set { if (value.Length > 2) User.SetUserName(value); } }
        public string UserCity {
            get => userCity; set {
                if (User.VerifyCity(value)) {
                    userCityStatusChangingTextColor = Color.Green;
                    NotifyPropertyChanged("UserCityStatusChangingTextColor");
                    User.SetUserCity(value);
                } else {
                    userCityStatusChangingTextColor = Color.Red;
                    NotifyPropertyChanged("UserCityStatusChangingTextColor");
                }
            }
        }
        public Color UserCityStatusChangingTextColor { get => userCityStatusChangingTextColor; }
        #endregion
        #region Commands
        public ICommand BackToMainPage { private set; get; }
        public ICommand Exit { private set; get; }
        #endregion
        #region ICommand realization
        public event EventHandler CanExecuteChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool CanExecute(object parameter) {
            throw new NotImplementedException();
        }
        public void Execute(object parameter) { }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}