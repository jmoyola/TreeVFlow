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
        
        String Text { get; set; }
        Color BackColor { get; set; }

        Control Header { get; set; }
        Control Footer { get; set; }
        
        void SuspendLayoutTree();
        void SuspendLayoutTreeNode();
        void ResumeLayoutTree();
        void ResumeLayoutTreeNode();
        
        IGraphicalTreeNode ParentTreeNode { get;}
        IGraphicalTreeNode RootTreeNode { get; }
        int TreeLevel { get; }
        IList<IGraphicalTreeNode> TreeNodes { get; }
        void ClearTreeNodes();
        IGraphicalTreeNode AddTreeNode(IGraphicalTreeNode newTreeNode);
        void RemoveTreeNode(IGraphicalTreeNode treeNodeToRemove);
        void RemoveTreeNode();
        IGraphicalTreeNode TreeDeepFirstOrDefault(Func<IGraphicalTreeNode, bool> predicate);
        IEnumerable<IGraphicalTreeNode> TreeDeepWhere(Func<IGraphicalTreeNode, bool> predicate);
        
        void AddContent(Control content);
        void RemoveContent(Control content);
        void ClearContent();
        IList<Control> TreeContent { get; }
        void ClearAll();
        bool IsExpanded { get; }
        void ToggleItems();
        void Collapse();
        void Expand();
        Control ContentDeepFirstOrDefault(Func<Control, bool> predicate);
        IEnumerable<Control> ContentDeepWhere(Func<Control, bool> predicate);
    }
}