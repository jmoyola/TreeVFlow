using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TreeVFlowControl.Core
{
    public delegate void TreeNodeEventHandler(object sender, TreeNodeEventArgs args);
    public class TreeNodeEventArgs : EventArgs
    {
        public TreeNodeEventArgs(IGraphicalTreeNode treeNode, Control content=null)
        {
            TreeNode = treeNode;
            Content=content;
        }
        public IGraphicalTreeNode TreeNode { get; internal set; }
        public Control Content { get; internal set; }
    }

    public interface IGraphicalTreeNodeEvents
    {
        event TreeNodeEventHandler TreeNodeHeaderClick;
        event TreeNodeEventHandler TreeNodeHeaderDoubleClick;
        event TreeNodeEventHandler TreeNodeFooterClick;
        event TreeNodeEventHandler TreeNodeFooterDoubleClick;
        event TreeNodeEventHandler ContentNodeClick;
        event TreeNodeEventHandler ContentNodeDoubleClick;
        event TreeNodeEventHandler TreeNodeExpandedChanged;
        event TreeNodeEventHandler TreeNodeVisibleChanged;
        event TreeNodeEventHandler ContentNodeVisibleChanged;
        event TreeNodeEventHandler TreeNodeEnabledChanged;
        event TreeNodeEventHandler ContentNodeEnabledChanged;
        event TreeNodeEventHandler TreeNodeAdded;
        event TreeNodeEventHandler TreeNodeRemoved;
        event TreeNodeEventHandler ContentNodeAdded;
        event TreeNodeEventHandler ContentNodeRemoved;
        event TreeNodeEventHandler TreeNodeRefresh;

    }

    public interface IGraphicalTreeNode:IGraphicalTreeNodeEvents
    {
        String Text { get; set; }
        Color BackColor { get; set; }
        bool Visible { get; set; }
        Control Header { get; set; }
        Control Footer { get; set; }
        IGraphicalTreeNode ParentTreeNode { get;}
        Panel PanelContainer { get; }
        IList<IGraphicalTreeNode> NodeBranch { get; }
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
        bool Expand { get; set; }
    }
}