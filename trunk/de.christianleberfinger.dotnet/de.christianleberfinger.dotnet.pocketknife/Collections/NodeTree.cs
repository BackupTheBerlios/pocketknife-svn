using System;
using System.Collections.Generic;
using System.Text;

namespace de.christianleberfinger.dotnet.pocketknife.Collections
{
    /// <summary>
    /// Implements a simple tree.
    /// </summary>
    /// <typeparam name="T">The type that you want to store in this tree.</typeparam>
    public class NodeTree<T> : INodeTree<T>
    {
        private T _data = default(T);
        private INodeTree<T> _parent = null;
        private List<INodeTree<T>> _children = new List<INodeTree<T>>();

        /// <summary>
        /// Gets the current node's children.
        /// </summary>
        public List<INodeTree<T>> Children
        {
            get { return _children; }
        }

        /// <summary>
        /// Creates a new tree.
        /// </summary>
        public NodeTree() { }

        /// <summary>
        /// Gets or sets the data that is connected to this node.
        /// </summary>
        public T Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// Returns the parent of the current node or null if the current node 
        /// is the tree's root.
        /// </summary>
        public INodeTree<T> Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// Gets the root of this tree.
        /// </summary>
        public INodeTree<T> Root
        {
            get
            {
                INodeTree<T> node = this;
                while (node.Parent != null)
                    node = node.Parent;

                return node;
            }
        }

        /// <summary>
        /// Indicates whether the current node is the root of the tree.
        /// </summary>
        public bool IsRoot
        {
            get { return _parent == null; }
        }

        /// <summary>
        /// Resets the current tree to it's root.
        /// </summary>
        public void clear()
        {
            _data = default(T);
            _parent = null;
            _children.Clear();
        }

        /// <summary>
        /// Gets the current node's siblings. (all nodes with the same parent, including the node itself)
        /// </summary>
        public List<INodeTree<T>> Siblings
        {
            get
            {
                if (IsRoot)
                    return new List<INodeTree<T>>();

                return _parent.Children;
            }
        }

    }
}
