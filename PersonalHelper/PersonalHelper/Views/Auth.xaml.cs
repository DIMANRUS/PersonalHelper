using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using PersonalHelper.Models;
using PersonalHelper.Helpers;

namespace PersonalHelper.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Auth : ContentPage
    {
        RootJsonCities cities;
        public Auth()
        {
            InitializeComponent();
            cities = FilesHelper.GetRootJsonCities();
        }
        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            switch (((RadioButton)sender).Content)
            {
                case "Системный":
                    Application.Current.UserAppTheme = OSAppTheme.Unspecified;
                    break;
                case "Тёмный":
                    Preferences.Set("UserTheme", "Dark");
                    Application.Current.UserAppTheme = OSAppTheme.Dark;
                    break;
                case "Светлый":
                    Preferences.Set("UserTheme", "Light");
                    Application.Current.UserAppTheme = OSAppTheme.Light;
                    break;
            }
        }
        private async void ShowLocationInformation_Clicked(object sender, System.EventArgs e) => await DisplayAlert("Зачем это нужно?", "На основе вашего введённого города будет показываться погода, новости и другая информация", "Ок");

        private async void NavigateToMainPage_Click(object sender, System.EventArgs e)
        {
            if (User.VerifyCity(city.Text))
            {
                if (name.Text.Length == 0)
                    await DisplayAlert("Ошибка", "Введите имя", "Ок");
                else
                {
                    User.SetUserName(name.Text);
                    User.SetUserCity(city.Text);
                    await Navigation.PushModalAsync(new MainPage(), true);
                }
            }
            else
                await DisplayAlert("Ошибка", "Город не найден", "Ок");
        }
    }
}