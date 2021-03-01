using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PersonalHelper.Models {
    class Wheather {
        public string City { get; set; }
        public double Temperature { get; set;}
        public Image Image { get; set; } = new Image();
    }
}