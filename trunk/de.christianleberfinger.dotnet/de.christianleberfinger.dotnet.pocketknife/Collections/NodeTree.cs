/*
 * 
 * Copyright (c) 2007 Christian Leberfinger
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a 
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software 
 * is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN 
 * AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
 * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */
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
        private NodeTree<T> _parent = null;
        private List<INodeTree<T>> _children = new List<INodeTree<T>>();

        /// <summary>
        /// Gets the current node's children.
        /// </summary>
        public List<INodeTree<T>>.Enumerator Children
        {
            get { return _children.GetEnumerator(); }
        }

        /// <summary>
        /// Creates an empty tree.
        /// </summary>
        public NodeTree() { }

        /// <summary>
        /// Adds a childnode to the current node.
        /// </summary>
        /// <param name="child">The child to add.</param>
        public void addChild(NodeTree<T> child)
        {
            child._parent = this;
            _children.Add(child);            
        }

        /// <summary>
        /// Adds a new childnode to the current node.
        /// </summary>
        /// <param name="childData">The data the new node contains.</param>
        public void addChild(T childData)
        {
            NodeTree<T> child = new NodeTree<T>();
            child.Data = childData;
            addChild(child);
        }

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
        /// Gets the depth of the current node. The depth is the distance from the current node to the root of the tree.
        /// That means how deep the current node is 'hidden' in the hierarchy of the tree. Root has a depth of 0,
        /// a child of root has a depth of 1.
        /// </summary>
        public int Depth
        {
            get
            {
                int depth = 0;
                INodeTree<T> node = this;
                while (node.Parent != null)
                {
                    depth++;
                    node = node.Parent;
                }

                return depth;
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
        public List<INodeTree<T>>.Enumerator Siblings
        {
            get
            {
                if (IsRoot)
                    return new List<INodeTree<T>>().GetEnumerator();

                return _parent.Children;
            }
        }

        /// <summary>
        /// Gets the sibling before the current node or null if there isn't a previous sibling.
        /// </summary>
        public INodeTree<T> PreviousSibling
        {
            get
            {
                List<INodeTree<T>> siblings = _parent._children;

                // if there's less than 2 siblings, the current node hasn't one
                if (siblings.Count < 2)
                    return null;

                int currentIndex = siblings.IndexOf(this);

                // the first sibling hasn't a previous
                if (currentIndex == 0)
                    return null;
                else
                    return siblings[currentIndex - 1];
            }
        }

        /// <summary>
        /// Gets the sibling after the current node or null if there isn't a next sibling.
        /// </summary>
        public INodeTree<T> NextSibling
        {
            get
            {
                List<INodeTree<T>> siblings = _parent._children;

                // if there's less than 2 siblings, the current node hasn't one
                if (siblings.Count < 2)
                    return null;

                int currentIndex = siblings.IndexOf(this);

                // the last sibling hasn't a next
                if (currentIndex > siblings.Count)
                    return null;
                else
                    return siblings[currentIndex + 1];
            }
        }

        /// <summary>
        /// Return the number of children the current node has. (also called out-degree)
        /// </summary>
        public int Degree
        {
            get { return _children.Count; }
        }

    }
}
