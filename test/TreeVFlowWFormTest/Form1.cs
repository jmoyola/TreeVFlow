using System;

using System.Drawing;
using System.Windows.Forms;
using TreeVFlowControl.Core;
using TreeVFlowControl.Imp;


namespace TreeVFlowWFormTest
{
    public partial class Form1 : Form
    {
        private int _count;
        private TreeVFlowNode _currentTreeNode;
        private Control _currentContentNode;
        
        public Form1()
        {
            InitializeComponent();
            _treeVFlowNode1.AutoSize = false;
            _treeVFlowNode1.LevelIndent = 5;
            
            
            _currentTreeNode = _treeVFlowNode1;
            JoinAllEvents(_treeVFlowNode1);
        }

        private void JoinAllEvents(TreeVFlowNode node)
        {
            node.TreeNodeSelected += (_, a) =>
            {
                _currentTreeNode = a.TreeNode as TreeVFlowNode;
                lblTreeNode.Text=a.TreeNode.Text;
            };
            node.ContentNodeSelected += (_, args) =>
            {
                _currentContentNode = args.Content;
                lblContentNode.Text=args.Content.Text;
            };
            node.TreeNodeAdded +=TreeVFlowNode1_TreeNodeAdded;
            node.TreeNodeRefresh +=TreeVFlowNode1_TreeNodeRefresh;
            
            node.TreeNodeFold +=TreeVFlowNode1_TreeNodeRefresh;
            node.TreeNodeUnfold +=TreeVFlowNode1_TreeNodeRefresh;
        }

        private void TreeVFlowNode1_TreeNodeAdded(object sender, TreeNodeEventArgs args)
        {
            if(args.TreeNode.TreeLevel==1)
                args.TreeNode.BackColor = Color.Yellow;
            else if (args.TreeNode.TreeLevel == 2)
                args.TreeNode.BackColor = Color.Gray;
            else if (args.TreeNode.TreeLevel >= 3)
                args.TreeNode.BackColor = Color.White;
            TreeVFlowNode1_TreeNodeRefresh(sender, args);
        }
        private void TreeVFlowNode1_TreeNodeRefresh(object sender, TreeNodeEventArgs args)
        {
            Label headerLabel = args.TreeNode.Header as Label;
            if(headerLabel!=null)
                headerLabel.Text= $"{(args.TreeNode.IsFold?"+ ":"- ")} {args.TreeNode.Text} ({args.TreeNode.TreeNodes.Count + args.TreeNode.TreeContent.Count})";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (_currentTreeNode == null)
                return;
            
            _count ++;
            TreeVFlowNode nn = new TreeVFlowNode {Text= "Header " + _count , Height = 30, Header= new Label(){Text = "Header"  + _count, Height=30}, Footer = new Label(){Text = "Footer"  + _count, Height=30}};
            _currentTreeNode.AddTreeNode(nn);
            

            
        }



        private void button2_Click(object sender, EventArgs e)
        {
            _currentTreeNode?.RemoveTreeNode();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _currentTreeNode?.AddContent(new TextBox(){Height = 30});
        }

        private void button4_Click(object sender, EventArgs e)
        {
            (_currentContentNode?.Parent as TreeVFlowNode)?.RemoveContent(_currentContentNode);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(_currentTreeNode!=null)
                _treeVFlowNode1.ScrollShowTreeNode(_currentTreeNode);
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(_currentContentNode!=null)
                _treeVFlowNode1.ScrollShowContentNode(_currentContentNode);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(_currentTreeNode!=null && _currentTreeNode.Header!=null)
                _currentTreeNode.Header.Visible=!_currentTreeNode.Header.Visible;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(_currentTreeNode!=null && _currentTreeNode.Footer!=null)
                _currentTreeNode.Footer.Visible=!_currentTreeNode.Footer.Visible;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(_currentContentNode!=null)
                _currentContentNode.Visible=!_currentContentNode.Visible;
        }
    }
}
