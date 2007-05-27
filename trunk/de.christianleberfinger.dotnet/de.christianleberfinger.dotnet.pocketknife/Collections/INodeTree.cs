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
    /// Defines the operations available on a simple tree.
    /// </summary>
    public interface INodeTree<T>
    {
        /// <summary>
        /// Gets or sets the data object connected to this node.
        /// </summary>
        T Data { get; set; }

        /// <summary>
        /// Gets the parent of the current node or null if the current node 
        /// is the tree's root.
        /// </summary>
        INodeTree<T> Parent { get; }

        /// <summary>
        /// Resets the current tree to it's root.
        /// </summary>
        void clear();

        /// <summary>
        /// Indicates whether the current node is the root of the tree.
        /// </summary>
        bool IsRoot { get; }

        /// <summary>
        /// Gets the root of this tree.
        /// </summary>
        INodeTree<T> Root { get; }

        /// <summary>
        /// Gets the current node's children.
        /// </summary>
        List<INodeTree<T>> Children { get; }

        /// <summary>
        /// Gets the current node's siblings. (all nodes with the same parent, including the node itself)
        /// </summary>
        List<INodeTree<T>> Siblings { get; }
    }
}
