using PersonalHelper.ViewModels;
using Xamarin.Forms;

namespace PersonalHelper
{
    public partial class MainPage : ContentPage
    {
        MainPageVM mainPageModel;

        public MainPage() 
        {
            InitializeComponent();
            mainPageModel = new MainPageVM();        
            this.BindingContext = mainPageModel;
        }
    }
}