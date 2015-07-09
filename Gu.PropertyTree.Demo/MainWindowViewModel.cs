namespace Gu.PropertyTree.Demo
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class MainWindowViewModel
    {
        private readonly ObservableCollection<RootNode> _root = new ObservableCollection<RootNode>();

        public MainWindowViewModel()
        {
            var dummy1 = new Dummy { Name = "Max", Value = 24 };
            var dummy2 = new Dummy { Name = "Johan", Value = 23 };
            var dummy3 = new Dummy { Name = "Urban", Value = 24 };
            dummy1.Dummies = new List<Dummy>() { dummy2, dummy3 };
            var l = new List<Dummy>() { dummy1,dummy2 };
          
            var rootNode = Node.Create(l);
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
