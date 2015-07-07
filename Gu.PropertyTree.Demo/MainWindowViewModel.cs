namespace Gu.PropertyTree.Demo
{
    using System.Collections.ObjectModel;

    public class MainWindowViewModel
    {
        private readonly ObservableCollection<RootNode> _root = new ObservableCollection<RootNode>();

        public MainWindowViewModel()
        {
            var instance = new Dummy { Name = "Max", Value = 24 };
            var rootNode = Node.Create(instance);
            Root.Add(rootNode);
        }

        public ObservableCollection<RootNode> Root
        {
            get
            {
                return _root;
            }
        }
    }
}
