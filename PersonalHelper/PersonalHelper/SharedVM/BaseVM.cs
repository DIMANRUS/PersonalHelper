﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace PersonalHelper.SharedVM {
    public class BaseVM : INotifyPropertyChanged {
        protected Page CurrentPage { get => Application.Current.MainPage; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}