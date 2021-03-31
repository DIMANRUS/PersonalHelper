using PersonalHelper.Models;
using PersonalHelper.SharedVM;
using PersonalHelper.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace PersonalHelper.ViewModels {
    class SettingsPageVM : BaseVM
    {
        #region Commands realization
        public SettingsPageVM()
        {
            if (User.GetUserTheme() == "Dark")
            {
                _IsCheckedRadioButtonDarkTheme = "True"; NotifyPropertyChanged(nameof(IsCheckedRadioButtonDarkTheme));
            }
            else
            {
                _IsCheckedRadioButtonLightTheme = "True"; NotifyPropertyChanged(nameof(IsCheckedRadioButtonLightTheme));
            }
            Exit = new Command(execute: async () =>
            {
                User.ClearUserData();
                await CurrentPage.Navigation.PushAsync(new Auth(), true);
            });
        }
        #endregion
        #region Private field
        private string userCity = User.GetUserCity();
        private Color userCityStatusChangingTextColor = Color.Green;
        private string _IsCheckedRadioButtonDarkTheme = "False", _IsCheckedRadioButtonLightTheme = "False";
        #endregion
        #region Properties
        public string UserName { get => User.GetUserName(); set { if (value.Length > 2) User.SetUserName(value); } }
        public string UserCity
        {
            get => userCity; set
            {
                if (User.VerifyCity(value))
                {
                    userCityStatusChangingTextColor = Color.Green;
                    NotifyPropertyChanged("UserCityStatusChangingTextColor");
                    User.SetUserCity(value);
                }
                else
                {
                    userCityStatusChangingTextColor = Color.Red;
                    NotifyPropertyChanged("UserCityStatusChangingTextColor");
                }
            }
        }
        public Color UserCityStatusChangingTextColor { get => userCityStatusChangingTextColor; }
        public string IsCheckedRadioButtonDarkTheme
        {
            get => _IsCheckedRadioButtonDarkTheme;
        }
        public string IsCheckedRadioButtonLightTheme
        {
            get => _IsCheckedRadioButtonLightTheme;
        }
        #endregion
        #region Commands
        public ICommand Exit { private set; get; }
        #endregion
    }
}