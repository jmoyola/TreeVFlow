using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TreeVFlowControl.Core
{
    public delegate void TreeNodeEventHandler(object sender, TreeNodeEventArgs args);
    public class TreeNodeEventArgs : EventArgs
    {
        public TreeNodeEventArgs(IGraphicalTreeNode treeNode)
        {
            TreeNode = treeNode;
        }
        public IGraphicalTreeNode TreeNode { get; internal set; }
    }

    public enum LoadingStatus { NotLoaded, PartiallyLoaded, Loaded}
    public enum UpdateStatus { Node, Updated }
    public interface IGraphicalTreeNode
    {
        event TreeNodeEventHandler TreeNodeHeaderClick;
        event TreeNodeEventHandler TreeNodeHeaderDoubleClick;
        event TreeNodeEventHandler TreeNodeFooterClick;
        event TreeNodeEventHandler TreeNodeFooterDoubleClick;
        event TreeNodeEventHandler ContentNodeClick;
        event TreeNodeEventHandler ContentNodeDoubleClick;
        event TreeNodeEventHandler TreeNodeCollapsed;
        event TreeNodeEventHandler TreeNodeExpanded;
        event TreeNodeEventHandler TreeNodeAdded;
        event TreeNodeEventHandler TreeNodeRemoved;
        event TreeNodeEventHandler ContentNodeAdded;
        event TreeNodeEventHandler ContentNodeRemoved;
        event TreeNodeEventHandler TreeNodeRefresh;
        event TreeNodeEventHandler ResizeHeight;
        event TreeNodeEventHandler ResizeWidth;
        
        String Text { get; set; }
        Color BackColor { get; set; }

        Control Header { get; set; }
        Control Footer { get; set; }
        
        IGraphicalTreeNode ParentTreeNode { get;}
        IGraphicalTreeNode RootTreeNode { get; }
        int TreeLevel { get; }
        IList<IGraphicalTreeNode> TreeNodes { get; }
        void ClearTreeNodes();
        IGraphicalTreeNode AddTreeNode(IGraphicalTreeNode newTreeNode);
        void RemoveTreeNode(IGraphicalTreeNode treeNodeToRemove);
        
        IGraphicalTreeNode TreeDeepFirstOrDefault(Func<IGraphicalTreeNode, bool> predicate);
        IEnumerable<IGraphicalTreeNode> TreeDeepWhere(Func<IGraphicalTreeNode, bool> predicate);
        Control ContentDeepFirstOrDefault(Func<Control, bool> predicate);
        IEnumerable<Control> ContentDeepWhere(Func<Control, bool> predicate);
        
        void AddContent(Control content);
        void RemoveContent(Control content);
        void ClearContent();
        IList<Control> TreeContent { get; }
        void ClearAll();
        bool IsExpanded { get; }
        void ToggleItems();
        void Collapse();
        void Expand();

    }
}