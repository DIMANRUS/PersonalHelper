using Xamarin.Essentials;

namespace PersonalHelper.Models {
    static class User
    {
        public static string GetUserName() => Preferences.Get("UserName","");
        public static void SetUserName(string NewUserName) => Preferences.Set("UserName", NewUserName);
    }
}