namespace Gu.PropertyTree.Demo
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Gu.PropertyTree.Annotations;

    public class Dummy: INotifyPropertyChanged
    {
        private string _name;
        private int _value;
        private Dummy _next;
        private int _value2;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name)
                {
                    return;
                }
                _name = value;
                OnPropertyChanged();
            }
        }

        public int Value
        {
            get { return _value; }
            set
            {
                if (value == _value)
                {
                    return;
                }
                _value = value;
                OnPropertyChanged();
            }
        }

        public Dummy Next
        {
            get { return _next; }
            set
            {
                if (Equals(value, _next))
                {
                    return;
                }
                _next = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<Dummy> Dummies { get; set; }

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