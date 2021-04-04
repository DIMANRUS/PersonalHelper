using System;

namespace PersonalHelper.Droid {
    class NotificationEventArgs : EventArgs {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}