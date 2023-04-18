using PersonalHelper.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PersonalHelper.Models {
    static class User {
        public static string GetUserName() => Preferences.Get("UserName", "null");
        public static void SetUserName(string NewUserName) => Preferences.Set("UserName", NewUserName);
        public static string GetUserCity() => Preferences.Get("UserCity", "null");
        public static void SetUserCity(string NewUserCity) => Preferences.Set("UserCity", NewUserCity);
        public static string GetUserTheme() => Preferences.Get("UserTheme", "null");
        public static void SetUserTheme(string NewUserTheme) => Preferences.Set("UserTheme", NewUserTheme);
        public static string GetUserZodiakString() => Int32.Parse(Preferences.Get("UserZodiak", "null")) switch { 
            0 => "Скорпион",
            1 => "Овен",
            2 => "Стрелец",
            3 => "Лев",
            4 => "Дева",
            5 => "Весы",
            6 => "Козерог",
            7 => "Водолей",
            8 => "Рыбы",
            9 => "Рак",
            10 => "Близнецы",
            11 => "Телец"
        };
        public static string GetUserZodiakId() => Preferences.Get("UserZodiak", "null");
        public static void SetUserZodiak(string NewUserZodiak) => Preferences.Set("UserZodiak", NewUserZodiak);
        public static async Task<bool> VerifyCity(string NameCity) =>
             ((await FilesHelper.GetRootJsonCities()).FirstOrDefault(x => x.undefined == NameCity) == null) ? false : true;
        public static void ClearUserData() => Preferences.Clear();
        public static void AddUserNewsKeyword(string keyword) => Preferences.Set("UserNewsKeyword", Preferences.Get("UserNewsKeyword", "") + "/" + keyword);
        public static string[] GetUserNewsKeyword() => Preferences.Get("UserNewsKeyword", "").Split('/');
        public static void DeleteNewsKeyWord(string keyword) => Preferences.Set("UserNewsKeyword", Preferences.Get("UserNewsKeyword", "").Replace($"{keyword}", ""));
    }
}