﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PersonalHelper.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToDoPage : TabbedPage {
        public ToDoPage() =>
            InitializeComponent();
    }
}