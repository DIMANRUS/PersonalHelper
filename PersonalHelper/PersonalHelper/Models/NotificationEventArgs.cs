namespace PersonalHelper.Models;

class NotificationEventArgs : EventArgs
{
    public string Title { get; set; }
    public string Message { get; set; }
}