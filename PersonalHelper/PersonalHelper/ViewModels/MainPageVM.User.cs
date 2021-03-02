using System;
using System.Collections.Generic;
using System.Text;
using PersonalHelper.Models;

namespace PersonalHelper.ViewModels {
    partial class MainPageVM {
        public string HelloUserName { get => "Доброе утро, " + User.GetUserName(); }
    }
}