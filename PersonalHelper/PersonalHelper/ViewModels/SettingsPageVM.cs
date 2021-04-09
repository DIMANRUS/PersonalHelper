using PersonalHelper.Models;
using PersonalHelper.SharedVM;
using PersonalHelper.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PersonalHelper.ViewModels {
    class SettingsPageVM : BaseVM {
        #region Commands realization
        public SettingsPageVM() {
            if (User.GetUserTheme() == "Dark") {
                _IsCheckedRadioButtonDarkTheme = "True";
                NotifyPropertyChanged(nameof(IsCheckedRadioButtonDarkTheme));
            } else {
                _IsCheckedRadioButtonLightTheme = "True";
                NotifyPropertyChanged(nameof(IsCheckedRadioButtonLightTheme));
            }
            Exit = new Command(async () => {
                User.ClearUserData();
                TodoItemDatabase db = await TodoItemDatabase.Instance;
                await db.DeleteAllItems();
                await CurrentPage.Navigation.PushAsync(new Auth(), true);
            });
            ChangedCity = new Command<string>(async (string NameCity) => {
                NameCity = NameCity.Replace(" ", "").ToLower();
                if (await User.VerifyCity(NameCity)) {
                    userCityStatusChangingTextColor = Color.Green;
                    NotifyPropertyChanged(nameof(UserCityStatusChangingTextColor));
                    User.SetUserCity(NameCity);
                } else {
                    userCityStatusChangingTextColor = Color.Red;
                    NotifyPropertyChanged(nameof(UserCityStatusChangingTextColor));
                }
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
        public Color UserCityStatusChangingTextColor { get => userCityStatusChangingTextColor; }
        public Command ChangedCity { get; set; }
        public string UserCity { get; } = User.GetUserCity();
        public string IsCheckedRadioButtonDarkTheme {
            get => _IsCheckedRadioButtonDarkTheme;
        }
        public string IsCheckedRadioButtonLightTheme {
            get => _IsCheckedRadioButtonLightTheme;
        }
        #endregion
        #region Commands
        public ICommand Exit { private set; get; }
        #endregion
    }
}