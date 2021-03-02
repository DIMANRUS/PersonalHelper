using PersonalHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
//giughoiuehrgeiorugtergtrt
namespace PersonalHelper {
    public partial class MainPage : ContentPage {
        MainPageVM mainPageModel;
        public MainPage() {
            InitializeComponent();
            mainPageModel = new MainPageVM();        
            this.BindingContext = mainPageModel;
        }
    }
}