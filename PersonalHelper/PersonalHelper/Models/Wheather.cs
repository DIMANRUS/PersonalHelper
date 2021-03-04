using Xamarin.Forms;

namespace PersonalHelper.Models {
    public class Wheather 
    {
        public string City { get; set; }
        public double Temperature { get; set;}
        public Image Image { get; set; } = new Image();
    }
}