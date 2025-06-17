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

    public delegate void CancellableEventHandler<T>(object sender, CancellableEventArgs<T> args) where T: EventArgs;
    public class CancellableEventArgs<T> where T: EventArgs
    {
        private readonly T _args;
        public CancellableEventArgs(T args)
        {
            _args = args;
        }
        
        public T Args=>_args;
        public bool Cancel { get; set; }
    }

    public interface IGraphicalTreeNodeEvents
    {
        event TreeNodeEventHandler TreeNodeHeaderClick;
        event TreeNodeEventHandler TreeNodeHeaderDoubleClick;
        event TreeNodeEventHandler TreeNodeFooterClick;
        event TreeNodeEventHandler TreeNodeFooterDoubleClick;
        event TreeNodeEventHandler ContentNodeClick;
        event TreeNodeEventHandler ContentNodeDoubleClick;
        event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeExpandedChanged;
        event TreeNodeEventHandler AfterTreeNodeExpandedChanged;
        event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeAdded;
        event TreeNodeEventHandler AfterTreeNodeAdded;
        event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeRemoved;
        event TreeNodeEventHandler AfterTreeNodeRemoved;
        event CancellableEventHandler<TreeNodeEventArgs> BeforeContentNodeAdded;
        event TreeNodeEventHandler AfterContentNodeAdded;
        event CancellableEventHandler<TreeNodeEventArgs> BeforeContentNodeRemoved;
        event TreeNodeEventHandler AfterContentNodeRemoved;
        event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeEnabledChanged;
        event TreeNodeEventHandler AfterTreeNodeEnabledChanged;
        event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeVisibleChanged;
        event TreeNodeEventHandler AfterTreeNodeVisibleChanged;

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
        IGraphicalTreeNode RootTreeNode { get; }
        int TreeLevel { get; }
        IList<IGraphicalTreeNode> TreeNodes { get; }
        void ClearTreeNodes();
        IGraphicalTreeNode AddTreeNode(IGraphicalTreeNode newTreeNode);
        IGraphicalTreeNode InsertTreeNode(IGraphicalTreeNode newTreeNode, int index);
        IGraphicalTreeNode InsertTreeNode(IGraphicalTreeNode newTreeNode, Func<object, object, int> comparator);
        void RemoveTreeNode(IGraphicalTreeNode treeNodeToRemove);
        IGraphicalTreeNode TreeDeepFirstOrDefault(Func<IGraphicalTreeNode, bool> predicate);
        IEnumerable<IGraphicalTreeNode> TreeDeepWhere(Func<IGraphicalTreeNode, bool> predicate);
        Control ContentDeepFirstOrDefault(Func<Control, bool> predicate);
        IEnumerable<Control> ContentDeepWhere(Func<Control, bool> predicate);
        
        void AddContent(Control content);
        void RemoveContent(Control content);
        void ClearContentNodes();
        IList<Control> TreeContent { get; }
        void ClearAll();
        bool IsExpanded { get; }
        void ToggleItems();
        void SetExpanded(bool expanded);
        void SetEnabled(bool enabled);
        bool IsEnabled { get; }
        bool IsVisible { get; }
        void SetVisible(bool visible);
    }
}