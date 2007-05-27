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
