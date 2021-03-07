using PersonalHelper.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PersonalHelper.ViewModels {
    class SettingsVm : ICommand {
        #region Commands realization
        public SettingsVm() {
            BackToMainPage = new Command(execute: async () => { await CurrentPage.Navigation.PopAsync(); });
        }
        #endregion
        private Page CurrentPage { get => Application.Current.MainPage; }
        #region Properties
        public string UserName { get => User.GetUserName(); set => User.SetUserName(value); }
        #endregion
        #region Commands
        public ICommand BackToMainPage { private set; get; }
        #endregion
        #region ICommand realization
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) {
            throw new NotImplementedException();
        }
        public void Execute(object parameter) { }
        #endregion
    }
}