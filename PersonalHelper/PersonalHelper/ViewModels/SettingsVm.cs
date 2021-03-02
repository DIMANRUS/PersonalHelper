using PersonalHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalHelper.ViewModels {
    class SettingsVm {
        public string UserName { get => User.GetUserName(); }
    }
}