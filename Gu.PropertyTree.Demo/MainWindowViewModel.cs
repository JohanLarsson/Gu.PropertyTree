using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gu.PropertyTree.Demo
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Gu.PropertyTree.Annotations;

    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            var instance = new Dummy() {  Value = 24 };
            var rootNode = Node.Create(instance);
           
            
            NodeObjectList = new ObservableCollection<RootNode>();
            NodeObjectList.Add(rootNode);
        }

        public ObservableCollection<RootNode> NodeObjectList { get; set; }
    }

    public class Dummy: INotifyPropertyChanged
    {
        public string Name { get; set; }
        public int Value { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
