using PersonalHelper.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PersonalHelper.ViewModels {
    class SettingsVm : ICommand {
        public SettingsVm() {
            Exit = new Command(execute: async () => {
                await CurrentPage.DisplayAlert("Hello","fghfgh","OK");
            });
        }
        private Page CurrentPage { get => Application.Current.MainPage; }
        public string UserName { get => User.GetUserName(); }
        public ICommand Exit { private set; get; }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) {
            throw new NotImplementedException();
        }
        public void Execute(object parameter) { }
    }
}