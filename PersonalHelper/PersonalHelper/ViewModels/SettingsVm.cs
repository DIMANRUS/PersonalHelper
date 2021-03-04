using PersonalHelper.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PersonalHelper.ViewModels {
    class SettingsVm : ICommand {
        public SettingsVm() {
            BackToMainPage = new Command(execute: async () => { await CurrentPage.Navigation.PopAsync(); });
        }
        private Page CurrentPage { get => Application.Current.MainPage; }
        public string UserName { get => User.GetUserName(); }
        public ICommand BackToMainPage { private set; get; }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) {
            throw new NotImplementedException();
        }
        public void Execute(object parameter) { }
    }
}