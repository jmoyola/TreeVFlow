using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TreeVFlowControl.Core
{
    public delegate void TreeNodeEventHandler<T>(object sender, TreeNodeEventArgs<T> args);
    public class TreeNodeEventArgs<T> : EventArgs
    {
        public TreeNodeEventArgs(IGraphicalTreeNode<T> treeNode, Control content=null)
        {
            TreeNode = treeNode;
            Content=content;
        }
        public IGraphicalTreeNode<T> TreeNode { get; internal set; }
        public Control Content { get; internal set; }
    }
    public interface IGraphicalTreeNode<T>:ITreeNode<T>
    {
        event TreeNodeEventHandler<T> TreeNodeHeaderClick;
        event TreeNodeEventHandler<T> TreeNodeHeaderDoubleClick;
        event TreeNodeEventHandler<T> TreeNodeFooterClick;
        event TreeNodeEventHandler<T> TreeNodeFooterDoubleClick;
        event TreeNodeEventHandler<T> ContentNodeClick;
        event TreeNodeEventHandler<T> ContentNodeDoubleClick;
        event TreeNodeEventHandler<T> TreeNodeCollapsed;
        event TreeNodeEventHandler<T> TreeNodeExpanded;
        event TreeNodeEventHandler<T> TreeNodeAdded;
        event TreeNodeEventHandler<T> TreeNodeRemoved;
        event TreeNodeEventHandler<T> ContentNodeAdded;
        event TreeNodeEventHandler<T> ContentNodeRemoved;
        event TreeNodeEventHandler<T> TreeNodeRefresh;
        event TreeNodeEventHandler<T> ResizeHeight;
        event TreeNodeEventHandler<T> ResizeWidth;

        void Disable();
        void Enable();
        
        bool IsExpanded { get; }
        void ToggleItems();
        void Collapse();
        void Expand();

    }
}