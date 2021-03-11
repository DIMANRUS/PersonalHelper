using PersonalHelper.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PersonalHelper.ViewModels
{
    partial class MainPageVM : ICommand 
    {
        private readonly News newsModel = new News();
        public ObservableCollection<Article> NewsCollection { get; private set; }
        public ICommand OpenNews { get; private set; }
        public ICommand OpenAllNews { get; private set; }
    }
}