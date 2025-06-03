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
        public event TreeNodeEventHandler TreeNodeExpandedChanged;
        public event TreeNodeEventHandler TreeNodeVisibleChanged;
        public event TreeNodeEventHandler ContentNodeVisibleChanged;
        public event TreeNodeEventHandler TreeNodeEnabledChanged;
        public event TreeNodeEventHandler ContentNodeEnabledChanged;
        public event TreeNodeEventHandler TreeNodeAdded;
        public event TreeNodeEventHandler TreeNodeRemoved;
        public event TreeNodeEventHandler ContentNodeAdded;
        public event TreeNodeEventHandler ContentNodeRemoved;
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
            _rootNode.Expand=true;
            _rootNode.TreeNodeAdded+=(_, args) =>JoinAllEvents(args.TreeNode);
        }
        
        public TreeVFlowNode RootNode=> _rootNode;
        
        protected override void OnResize(EventArgs eventargs)
        {
            _rootNode.RefreshNodeLayout(true);
            base.OnResize(eventargs);
        }
        
        private void JoinAllEvents(IGraphicalTreeNode node)
        {
            node.TreeNodeAdded +=(_, args) => JoinAllEvents(args.TreeNode);
            node.TreeNodeAdded +=(_, args) => TreeNodeAdded?.Invoke(this, args);
            node.TreeNodeRemoved +=(_, args) => TreeNodeRemoved?.Invoke(this, args);
            node.TreeNodeRefresh +=(_, args) => TreeNodeRefresh?.Invoke(this, args);
            
            node.TreeNodeExpandedChanged += (_, args) => TreeNodeExpandedChanged?.Invoke(this, args);
            node.TreeNodeEnabledChanged += (_, args) => TreeNodeEnabledChanged?.Invoke(this, args);
            node.ContentNodeEnabledChanged += (_, args) => ContentNodeEnabledChanged?.Invoke(this, args);
            node.TreeNodeVisibleChanged += (_, args) => TreeNodeVisibleChanged?.Invoke(this, args);
            node.ContentNodeVisibleChanged += (_, args) => ContentNodeVisibleChanged?.Invoke(this, args);
            
            node.TreeNodeHeaderClick += (_, args) => TreeNodeHeaderClick?.Invoke(this, args);
            node.TreeNodeHeaderDoubleClick += (_, args) => TreeNodeHeaderDoubleClick?.Invoke(this, args);
            node.TreeNodeFooterClick += (_, args) => TreeNodeFooterClick?.Invoke(this, args);
            node.TreeNodeFooterDoubleClick += (_, args) => TreeNodeFooterDoubleClick?.Invoke(this, args);
            node.ContentNodeAdded +=(_, args) => ContentNodeAdded?.Invoke(this, args);
            node.ContentNodeRemoved +=(_, args) => ContentNodeRemoved?.Invoke(this, args);
            node.ContentNodeClick += (_, args) => ContentNodeClick?.Invoke(this, args);
            node.ContentNodeDoubleClick += (_, args) => ContentNodeDoubleClick?.Invoke(this, args);
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


    }
}