using System.Windows.Input;

namespace PersonalHelper.SharedVM {
    public class ToDoVM {
        public ICommand CompleteTask { get; set; }
        public ICommand RemoveTask { get; set; }
    }
}