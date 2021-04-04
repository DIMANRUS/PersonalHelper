using System;

namespace PersonalHelper.iOS {
    class NotificationEventArgs : EventArgs {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}