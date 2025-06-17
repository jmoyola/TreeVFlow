using System;
using TreeVFlowControl.Core;
using System.Windows.Forms;

namespace TreeVFlowControl.Imp
{
    public class TreeVFlowControl:Panel, IGraphicalTreeNodeEvents
    {
        private readonly TreeVFlowNode _rootNode=new ();
        
        public event TreeNodeEventHandler TreeNodeHeaderClick;
        public event TreeNodeEventHandler TreeNodeHeaderDoubleClick;
        public event TreeNodeEventHandler TreeNodeFooterClick;
        public event TreeNodeEventHandler TreeNodeFooterDoubleClick;
        public event TreeNodeEventHandler ContentNodeClick;
        public event TreeNodeEventHandler ContentNodeDoubleClick;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeCollapsed;
        public event TreeNodeEventHandler AfterTreeNodeCollapsed;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeExpandedChanged;
        public event TreeNodeEventHandler AfterTreeNodeExpandedChanged;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeAdded;
        public event TreeNodeEventHandler AfterTreeNodeAdded;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeRemoved;
        public event TreeNodeEventHandler AfterTreeNodeRemoved;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeContentNodeAdded;
        public event TreeNodeEventHandler AfterContentNodeAdded;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeContentNodeRemoved;
        public event TreeNodeEventHandler AfterContentNodeRemoved;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeVisibleChanged;
        public event TreeNodeEventHandler AfterTreeNodeVisibleChanged;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeEnabledChanged;
        public event TreeNodeEventHandler AfterTreeNodeEnabledChanged;
        public event TreeNodeEventHandler TreeNodeRefresh;

        
        public TreeVFlowControl()
        {
            Init();
            
        }

        private void Init()
        {
            AutoScroll = true;
            
            Controls.Add(_rootNode);
            _rootNode.Top = 0;
            _rootNode.Left = 0;
            
            _rootNode.ColumnCount = 1;
            _rootNode.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 341F));
            _rootNode.Footer = null;
            _rootNode.Header = null;
            _rootNode.LevelIndent = 5;
            _rootNode.RowStyles.Add(new RowStyle(SizeType.AutoSize, 20F));
            _rootNode.SetExpanded(true);
            _rootNode.AfterTreeNodeAdded+=(_, args) =>JoinAllEvents(args.TreeNode);
        }
        
        public TreeVFlowNode RootNode=> _rootNode;
        
        protected override void OnResize(EventArgs eventargs)
        {
            _rootNode.RefreshNodeLayout(true);
            base.OnResize(eventargs);
        }
        
        private void JoinAllEvents(IGraphicalTreeNode node)
        {
            node.AfterTreeNodeAdded +=(_, args) => JoinAllEvents(args.TreeNode);
            
            node.BeforeTreeNodeExpandedChanged += (_, args) => BeforeTreeNodeExpandedChanged?.Invoke(this, args);
            node.AfterTreeNodeExpandedChanged += (_, args) => AfterTreeNodeExpandedChanged?.Invoke(this, args);
            
            node.TreeNodeHeaderClick += (_, args) => TreeNodeHeaderClick?.Invoke(this, args);
            node.TreeNodeHeaderDoubleClick += (_, args) => TreeNodeHeaderDoubleClick?.Invoke(this, args);
            node.TreeNodeFooterClick += (_, args) => TreeNodeFooterClick?.Invoke(this, args);
            node.TreeNodeFooterDoubleClick += (_, args) => TreeNodeFooterDoubleClick?.Invoke(this, args);
            node.ContentNodeClick += (_, args) => ContentNodeClick?.Invoke(this, args);
            node.ContentNodeDoubleClick += (_, args) => ContentNodeDoubleClick?.Invoke(this, args);
            
            
            node.BeforeTreeNodeAdded +=(_, args) => BeforeTreeNodeAdded?.Invoke(this, args);
            node.AfterTreeNodeAdded +=(_, args) => AfterTreeNodeAdded?.Invoke(this, args);
            node.BeforeTreeNodeRemoved +=(_, args) => BeforeTreeNodeRemoved?.Invoke(this, args);
            node.AfterTreeNodeRemoved +=(_, args) => AfterTreeNodeRemoved?.Invoke(this, args);
            node.BeforeContentNodeAdded +=(_, args) => BeforeContentNodeAdded?.Invoke(this, args);
            node.AfterContentNodeAdded +=(_, args) => AfterContentNodeAdded?.Invoke(this, args);
            node.BeforeContentNodeRemoved +=(_, args) => BeforeContentNodeRemoved?.Invoke(this, args);
            node.AfterContentNodeRemoved +=(_, args) => AfterContentNodeRemoved?.Invoke(this, args);
            node.BeforeTreeNodeEnabledChanged +=(_, args) => BeforeTreeNodeEnabledChanged?.Invoke(this, args);
            node.AfterTreeNodeEnabledChanged +=(_, args) => AfterTreeNodeEnabledChanged?.Invoke(this, args);
            node.BeforeTreeNodeVisibleChanged +=(_, args) => BeforeTreeNodeVisibleChanged?.Invoke(this, args);
            node.AfterTreeNodeVisibleChanged +=(_, args) => AfterTreeNodeVisibleChanged?.Invoke(this, args);
            node.TreeNodeRefresh +=(_, args) => TreeNodeRefresh?.Invoke(this, args);
        }
        
        public void ScrollShowTreeNode(IGraphicalTreeNode treeNode)
        {
            if(treeNode is TreeVFlowNode treeVFlowNode)
                ScrollControlIntoView(treeVFlowNode);
        }
        
        public void ScrollShowTreeNode(Func<IGraphicalTreeNode, bool> predicate)
        {
            var treeNode = _rootNode.TreeDeepFirstOrDefault(predicate);
            ScrollShowTreeNode(treeNode);
        }
        
        public void ScrollShowContentNode(Control contentNode)
        {
            ScrollControlIntoView(contentNode);
        }
        public void ScrollShowContentNode(Func<Control, bool> predicate)
        {
            var contentNode = _rootNode.ContentDeepFirstOrDefault(predicate);
            if(contentNode != null)
                ScrollControlIntoView(contentNode);
        }

        public int PaginationContentNodes { get; set; } = 0;

    }
}