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
            node.TreeNodeAdded +=(sender, args) => OnTreeNodeAdded(args);
            node.TreeNodeRemoved +=(sender, args) => OnTreeNodeRemoved(args);
            node.TreeNodeRefresh +=(sender, args) => OnTreeNodeRefresh(args);
            node.TreeNodeFold += (sender, args) => OnTreeNodeFold(args);
            node.TreeNodeUnfold += (sender, args) => OnTreeNodeUnfold(args);
            node.TreeNodeSelected += (sender, args) => OnTreeNodeSelected(args);

            node.ContentNodeAdded +=(sender, args) => OnContentNodeAdded(args);
            node.ContentNodeRemoved +=(sender, args) => OnContentNodeRemoved(args);
            node.ContentNodeSelected += (s, args) => OnContentNodeSelected(args);
        }
    }
}