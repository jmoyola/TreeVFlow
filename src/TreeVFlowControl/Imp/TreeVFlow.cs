using TreeVFlowControl.Core;
using System.Windows.Forms;

namespace TreeVFlowControl.Imp
{
    public class TreeVFlow:TreeVFlowNode
    {
        public TreeVFlow()
        {
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.TreeNodeAdded+=(sender, args) =>JoinAllEvents(args.TreeNode);
        }
        
        private void JoinAllEvents(IGraphicalTreeNode node)
        {
            node.TreeNodeAdded +=(_, args) => OnTreeNodeAdded(args);
            node.TreeNodeRemoved +=(_, args) => OnTreeNodeRemoved(args);
            node.TreeNodeRefresh +=(_, args) => OnTreeNodeRefresh(args);
            node.TreeNodeFold += (_, args) => OnTreeNodeFold(args);
            node.TreeNodeUnfold += (_, args) => OnTreeNodeUnfold(args);
            node.TreeNodeSelected += (_, args) => OnTreeNodeSelected(args);

            node.ContentNodeAdded +=(_, args) => OnContentNodeAdded(args);
            node.ContentNodeRemoved +=(_, args) => OnContentNodeRemoved(args);
            node.ContentNodeSelected += (_, args) => OnContentNodeSelected(args);
        }
    }
}