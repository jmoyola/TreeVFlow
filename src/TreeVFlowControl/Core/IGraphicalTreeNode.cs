using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TreeVFlowControl.Core
{
    public delegate void TreeNodeEventHandler(object sender, TreeNodeEventArgs args);
    public class TreeNodeEventArgs : EventArgs
    {
        public TreeNodeEventArgs(IGraphicalTreeNode treeNode, Control content)
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
        event TreeNodeEventHandler TreeNodeFold;
        event TreeNodeEventHandler TreeNodeUnfold;
        event TreeNodeEventHandler TreeNodeAdded;
        event TreeNodeEventHandler TreeNodeRemoved;
        event TreeNodeEventHandler ContentNodeAdded;
        event TreeNodeEventHandler ContentNodeRemoved;
        event TreeNodeEventHandler TreeNodeRefresh;
        
        String Text { get; set; }
        Color BackColor { get; set; }

        Control Header { get; set; }
        Control Footer { get; set; }
        
        IGraphicalTreeNode ParentTreeNode { get;}
        IGraphicalTreeNode RootTreeNode { get; }
        int TreeLevel { get; }
        IList<IGraphicalTreeNode> TreeNodes { get; }
        IGraphicalTreeNode AddTreeNode(IGraphicalTreeNode newTreeNode);
        void RemoveTreeNode(IGraphicalTreeNode treeNodeToRemove);
        void RemoveTreeNode();
        IGraphicalTreeNode TreeDeepFirstOrDefault(Func<IGraphicalTreeNode, bool> predicate);
        IEnumerable<IGraphicalTreeNode> TreeDeepWhere(Func<IGraphicalTreeNode, bool> predicate);
        
        void AddContent(Control content);
        void RemoveContent(Control content);
        IList<Control> TreeContent { get; }
        bool IsFold { get; }
        void TogleFold();
        void Fold();
        void Unfold();
        Control ContentDeepFirstOrDefault(Func<Control, bool> predicate);
        IEnumerable<Control> ContentDeepWhere(Func<Control, bool> predicate);
    }
}