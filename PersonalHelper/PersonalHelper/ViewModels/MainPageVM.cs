using PersonalHelper.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PersonalHelper.ViewModels {
    partial class MainPageVM : ICommand {
        public MainPageVM() {
            OpenSettings = new Command(execute: async () => {
                await CurrentPage.Navigation.PushAsync(new Settings());
            });
        }
        private Page CurrentPage{ get => Application.Current.MainPage; }
        public ICommand OpenSettings { private set; get; }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) {
            throw new NotImplementedException();
        }
        public void Execute(object parameter) {
            throw new NotImplementedException();
        }
    }
}