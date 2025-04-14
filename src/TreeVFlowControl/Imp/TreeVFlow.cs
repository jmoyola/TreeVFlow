using System;
using TreeVFlowControl.Core;
using System.Windows.Forms;

namespace TreeVFlowControl.Imp
{
    public class TreeVFlow:TreeVFlowNode
    {
        public TreeVFlow()
        {
            Init();
            //JoinAllEvents(this);
            TreeNodeAdded+=(_, args) =>JoinAllEvents(args.TreeNode);
        }

        private void Init()
        {
            AutoSize = false;
            AutoScroll = true;
            Expand();
        }
        
        private void JoinAllEvents(IGraphicalTreeNode node)
        {
            node.TreeNodeAdded +=(_, args) => OnTreeNodeAdded(args);
            node.TreeNodeRemoved +=(_, args) => OnTreeNodeRemoved(args);
            node.TreeNodeRefresh +=(_, args) => OnTreeNodeRefresh(args);
            node.TreeNodeCollapsed += (_, args) => OnTreeNodeCollapsed(args);
            node.TreeNodeExpanded += (_, args) => OnTreeNodeExpanded(args);
            node.TreeNodeHeaderClick += (_, args) => OnTreeNodeHeaderClick(args);
            node.TreeNodeHeaderDoubleClick += (_, args) => OnTreeNodeHeaderDoubleClick(args);
            node.TreeNodeFooterClick += (_, args) => OnTreeNodeFooterClick(args);
            node.TreeNodeFooterDoubleClick += (_, args) => OnTreeNodeFooterDoubleClick(args);
            node.ContentNodeAdded +=(_, args) => OnContentNodeAdded(args);
            node.ContentNodeRemoved +=(_, args) => OnContentNodeRemoved(args);
            node.ContentNodeClick += (_, args) => OnContentNodeClick(args);
            node.ContentNodeDoubleClick += (_, args) => OnContentNodeDoubleClick(args);
        }

        public void ScrollShowTreeNode(IGraphicalTreeNode treeNode)
        {
            if(treeNode is TreeVFlowNode treeVFlowNode)
                ScrollControlIntoView(treeVFlowNode);
        }
        public void ScrollShowTreeNode(Func<IGraphicalTreeNode, bool> predicate)
        {
            var treeNode = TreeDeepFirstOrDefault(predicate);
            ScrollShowTreeNode(treeNode);
        }
        
        public void ScrollShowContentNode(Control contentNode)
        {
            ScrollControlIntoView(contentNode);
        }
        public void ScrollShowContentNode(Func<Control, bool> predicate)
        {
            var contentNode = ContentDeepFirstOrDefault(predicate);
            if(contentNode != null)
                ScrollControlIntoView(contentNode);
        }
    }
}