﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Plainion.Windows.Controls.Tree;
using Plainion.Windows.Mvvm;

namespace Plainion.Windows.Controls.Text
{
    class NavigationNode : BindableBase, INode, IDragDropSupport
    {
        private string myName;
        private bool myIsSelected;
        private bool myIsExpanded;

        public NavigationNode()
        {
            Children = new ObservableCollection<NavigationNode>();
        }

        public bool IsDragAllowed { get { return true; } }

        public bool IsDropAllowed { get { return true; } }

        public string Name
        {
            get { return myName; }
            set { SetProperty(ref myName, value); }
        }

        IEnumerable<INode> INode.Children
        {
            get { return Children; }
        }

        public ObservableCollection<NavigationNode> Children { get; private set; }

        public INode Parent { get; set; }

        public bool IsSelected
        {
            get { return myIsSelected; }
            set { SetProperty(ref myIsSelected, value); }
        }

        public bool IsExpanded
        {
            get { return myIsExpanded; }
            set { SetProperty(ref myIsExpanded, value); }
        }

        bool INode.Matches(string pattern)
        {
            if(pattern == "*")
            {
                return Name != null;
            }

            return Name != null && Name.Contains(pattern, StringComparison.OrdinalIgnoreCase);
        }
    }
}
