namespace PersonalHelper;

public partial class App
{
    public App()
    {
        InitializeComponent();
        _ = User.GetUserTheme() switch
        {
            "Dark" => Current.UserAppTheme = OSAppTheme.Dark,
            "Light" => Current.UserAppTheme = OSAppTheme.Light,
            _ => Current.UserAppTheme = OSAppTheme.Dark
        };
        if (User.GetUserName() == "null")
            MainPage = new NavigationPage(new Auth())
            {
                BarBackgroundColor = (User.GetUserTheme() == "Light") ? Color.White : Color.Black,
                BarTextColor = (User.GetUserTheme() == "Light") ? Color.Black : Color.White
            };
        else
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = (User.GetUserTheme() == "Light") ? Color.White : Color.Black,
                BarTextColor = (User.GetUserTheme() == "Light") ? Color.Black : Color.White
            };

        #region For IOS Simulator
        //User.SetUserCity("Москва");
        //User.SetUserName("Дмитрий");
        //User.SetUserTheme("Dark");
        //User.SetUserZodiak("0");
        //MainPage = new NavigationPage(new MainPage())
        //{
        //    BarBackgroundColor = (User.GetUserTheme() == "Light") ? Color.White : Color.Black,
        //    BarTextColor = (User.GetUserTheme() == "Light") ? Color.Black : Color.White
        //};
        #endregion
    }
}