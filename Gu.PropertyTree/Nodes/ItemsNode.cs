namespace Gu.PropertyTree
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public class ItemsNode : RootNodeBase
    {
        public ItemsNode(IEnumerable enumerable)
            : base(enumerable)
        {
        }
    }
}