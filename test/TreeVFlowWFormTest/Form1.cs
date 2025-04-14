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
            _treeVFlowNode1.Left = 0;
            _treeVFlowNode1.Top = 0;
            _treeVFlowNode1.Height = 60;
            splitContainer1.Panel1.Resize += (sender, args) => _treeVFlowNode1.Width = splitContainer1.Panel1.Width; 
            
            _currentTreeNode = _treeVFlowNode1;
            JoinAllEvents(_treeVFlowNode1);
        }

        private void JoinAllEvents(TreeVFlowNode node)
        {
            node.TreeNodeHeaderClick += (_, a) =>
            {
                _currentTreeNode = a.TreeNode as TreeVFlowNode;
                lblTreeNode.Text=a.TreeNode.Text;
            };
            node.ContentNodeClick += (_, args) =>
            {
                _currentContentNode = args.Content;
                lblContentNode.Text=args.Content.Text;
            };
            node.TreeNodeAdded +=TreeVFlowNode1_TreeNodeAdded;
            node.TreeNodeRefresh +=TreeVFlowNode1_TreeNodeRefresh;
            
            node.TreeNodeCollapsed +=TreeVFlowNode1_TreeNodeRefresh;
            node.TreeNodeExpanded +=TreeVFlowNode1_TreeNodeRefresh;
        }

        private void TreeVFlowNode1_TreeNodeAdded(object sender, TreeNodeEventArgs args)
        {
            if(args.TreeNode.TreeLevel==0)
                args.TreeNode.BackColor = Color.Aqua;
            else if (args.TreeNode.TreeLevel == 1)
                args.TreeNode.BackColor = Color.Yellow;
            else if (args.TreeNode.TreeLevel == 2)
                args.TreeNode.BackColor = Color.Gray;
            else if (args.TreeNode.TreeLevel == 3)
                args.TreeNode.BackColor = Color.White;
            else if (args.TreeNode.TreeLevel == 4)
                args.TreeNode.BackColor = Color.RosyBrown;
            else if (args.TreeNode.TreeLevel >= 5)
                args.TreeNode.BackColor = Color.Beige;
            TreeVFlowNode1_TreeNodeRefresh(sender, args);
            if (args.TreeNode.ParentTreeNode!=null)
                TreeVFlowNode1_TreeNodeRefresh(sender, new TreeNodeEventArgs(args.TreeNode.ParentTreeNode, null));
        }
        private void TreeVFlowNode1_TreeNodeRefresh(object sender, TreeNodeEventArgs args)
        {
            Label headerLabel = args.TreeNode.Header as Label;
            if(headerLabel!=null)
                headerLabel.Text= $"{(args.TreeNode.IsExpanded?"- ":"+ ")} {args.TreeNode.Text} ({args.TreeNode.TreeNodes.Count + args.TreeNode.TreeContent.Count})";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (_currentTreeNode == null)
                return;
            
            _count ++;
            TreeVFlowNode nn = new TreeVFlowNode {Text= "Header " + _count , Height = 30, Header= new Label(){Text = "Header"  + _count, Height=30, BorderStyle = BorderStyle.FixedSingle}, Footer = new Label(){Text = "Footer"  + _count, Height=30, BorderStyle = BorderStyle.FixedSingle}};
            nn.TreeNodeHeaderDoubleClick+=(o, args) => args.TreeNode.ToggleItems(); 
            _currentTreeNode.AddTreeNode(nn);
            

            
        }



        private void button2_Click(object sender, EventArgs e)
        {
            _currentTreeNode?.ParentTreeNode?.RemoveTreeNode(_currentTreeNode);
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
                _currentTreeNode.Header.Visible= (!_currentTreeNode.Header.Visible);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(_currentTreeNode!=null && _currentTreeNode.Footer!=null)
                _currentTreeNode.Footer.Visible= (!_currentTreeNode.Footer.Visible);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(_currentContentNode!=null)
                _currentContentNode.Visible= (!_currentContentNode.Visible);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(_currentTreeNode!=null)
                _currentTreeNode.Visible= (!_currentTreeNode.Visible);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (_currentTreeNode == null)
                return;
            
            _count ++;
            GroupItemNode nn = new GroupItemNode {Text= "Header " + _count , Height = 30};
            _currentTreeNode.AddTreeNode(nn);
        }
    }
}
