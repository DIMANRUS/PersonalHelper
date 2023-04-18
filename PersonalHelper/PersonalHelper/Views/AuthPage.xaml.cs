namespace PersonalHelper.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Auth : ContentPage
{
    public Auth()
    {
        InitializeComponent();
    }
    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        switch (((RadioButton)sender).Content)
        {
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
}