namespace Gu.PropertyTree.Demo
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class MainWindowViewModel
    {
        private readonly ObservableCollection<RootNode> _root = new ObservableCollection<RootNode>();

        public MainWindowViewModel()
        {
            var dummy2 = new Dummy { Name = "Johan", Value = 23 };
            var dummy1 = new Dummy { Name = "Max", Value = 24, Next = dummy2 };

            var rootNode = Node.Create(dummy1);
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
