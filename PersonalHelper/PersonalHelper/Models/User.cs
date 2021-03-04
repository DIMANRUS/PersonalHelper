using Xamarin.Essentials;

namespace PersonalHelper.Models {
    static class User {
        public static string GetUserName() => Preferences.Get("UserName","");
    }
}