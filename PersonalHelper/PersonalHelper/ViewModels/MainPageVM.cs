using PersonalHelper.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PersonalHelper.ViewModels {
    partial class MainPageVM : ICommand {
        private Page CurrentPage { get => Application.Current.MainPage; }
        public ICommand OpenSettings { get; } = new Command(execute: async () => { await Application.Current.MainPage.Navigation.PushAsync(new Settings()); });
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) {
            throw new NotImplementedException();
        }
        public void Execute(object parameter) {
            throw new NotImplementedException();
        }
    }
}