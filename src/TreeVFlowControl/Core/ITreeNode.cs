using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TreeVFlowControl.Core
{
    public interface ITreeNode<T>:INode<T>
    {
        ITreeNode<T> ParentTreeNode { get; internal set; }
        ITreeNode<T> RootTreeNode { get; }
        int TreeLevel { get; }
        IList<ITreeNode<T>> TreeNodes { get; }
        void ClearTreeNodes();
        ITreeNode<T> AddTreeNode();
        void RemoveTreeNode(ITreeNode<T> treeNodeToRemove);

        IList<INode<T>> TreeContentNodes { get; }
        void ClearTreeContentNodes();
        INode<T> AddTreeContentNode();
        void RemoveTreeContentNode(INode<T> treeNodeToRemove);

        ITreeNode<T> TreeDeepFirstOrDefault(Func<ITreeNode<T>, bool> predicate);
        IEnumerable<ITreeNode<T>> TreeDeepWhere(Func<ITreeNode<T>, bool> predicate);
        INode<T> TreeContentDeepFirstOrDefault(Func<INode<T>, bool> predicate);
        IEnumerable<INode<T>> TreeContentDeepWhere(Func<INode<T>, bool> predicate);
        void AddContent(T content);
        void RemoveContent(T content);
        void ClearContent();
        IList<T> TreeContent { get; }
        void ClearAll();
    }
}