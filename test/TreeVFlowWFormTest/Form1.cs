using System;

using System.Drawing;
using System.Windows.Forms;
using TreeVFlowControl.Core;
using TreeVFlowControl.Imp;


namespace TreeVFlowWFormTest
{
    public partial class Form1 : Form
    {
        private int _treeNodesCount;
        private int _contentsCount;
        private TreeVFlowNode _currentTreeNode;
        private Control _currentContentNode;
        
        public Form1()
        {
            InitializeComponent();

            _treeVFlowControl1.RootNode.LevelIndent = 5;
            _treeVFlowControl1.Dock = DockStyle.Fill;
            
            _currentTreeNode = _treeVFlowControl1.RootNode;
            JoinAllEvents();
        }

        private void JoinAllEvents()
        {
            _treeVFlowControl1.TreeNodeHeaderClick += (_, a) =>
            {
                _currentTreeNode = a.TreeNode as TreeVFlowNode;
                lblTreeNode.Text=a.TreeNode.Text;
            };
            _treeVFlowControl1.ContentNodeClick += (_, args) =>
            {
                _currentContentNode = args.Content;
                lblContentNode.Text=$"[{args.Content.Name}] {args.Content.Text}";
            };
            _treeVFlowControl1.TreeNodeAdded +=TreeVFlowNode1_TreeNodeAdded;
            _treeVFlowControl1.TreeNodeRefresh +=TreeVFlowNode1_TreeNodeRefresh;
            
            _treeVFlowControl1.TreeNodeCollapsed +=TreeVFlowNode1_TreeNodeRefresh;
            _treeVFlowControl1.TreeNodeExpanded +=TreeVFlowNode1_TreeNodeRefresh;
        }

        private void TreeVFlowNode1_TreeNodeAdded(object sender, TreeNodeEventArgs args)
        {
            if(args.TreeNode.TreeLevel==0)
                args.TreeNode.BackColor = Color.Yellow;
            else if (args.TreeNode.TreeLevel == 1)
                args.TreeNode.BackColor = Color.Gray;
            else if (args.TreeNode.TreeLevel == 2)
                args.TreeNode.BackColor = Color.White;
            else if (args.TreeNode.TreeLevel == 3)
                args.TreeNode.BackColor = Color.RosyBrown;
            else if (args.TreeNode.TreeLevel == 4)
                args.TreeNode.BackColor = Color.Beige;
            else if (args.TreeNode.TreeLevel >= 5)
                args.TreeNode.BackColor = Color.Aquamarine;
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


        private void addTreeNodeButton_Click(object sender, EventArgs e)
        {
            if (_currentTreeNode == null)
                return;
            
            _treeNodesCount ++;
            TreeVFlowNode nn = new TreeVFlowNode {Text= "Header " + _treeNodesCount , Header= new Label(){Text = "Header"  + _treeNodesCount, Height=30, BorderStyle = BorderStyle.FixedSingle}, Footer = new Label(){Text = "Footer"  + _treeNodesCount, Height=30, BorderStyle = BorderStyle.FixedSingle}};
            nn.TreeNodeHeaderDoubleClick+=(o, args) => args.TreeNode.ToggleItems(); 
            _currentTreeNode.AddTreeNode(nn);
            

            
        }



        private void removeTreeNodeButton_Click(object sender, EventArgs e)
        {
            _currentTreeNode?.ParentTreeNode?.RemoveTreeNode(_currentTreeNode);
        }

        private void addContentButton_Click(object sender, EventArgs e)
        {
            _contentsCount++;
            _currentTreeNode?.AddContent(new TextBox(){Height = 30, Name = $"Content {_contentsCount}", Text = $"Content {_contentsCount}"});
        }

        private void removeContentButton_Click(object sender, EventArgs e)
        {
            (_currentContentNode?.Parent as TreeVFlowNode)?.RemoveContent(_currentContentNode);
        }

        private void showActiveTreeNodeButton_Click(object sender, EventArgs e)
        {
            if(_currentTreeNode!=null)
                _treeVFlowControl1.ScrollShowTreeNode(_currentTreeNode);
            
        }

        private void showActiveContentButton_Click(object sender, EventArgs e)
        {
            if(_currentContentNode!=null)
                _treeVFlowControl1.ScrollShowContentNode(_currentContentNode);
        }

        private void activeTreeNodeHeaderVisibleToggleButton_Click(object sender, EventArgs e)
        {
            if(_currentTreeNode!=null && _currentTreeNode.Header!=null)
                _currentTreeNode.Header.Visible= (!_currentTreeNode.Header.Visible);
        }

        private void activeTreeNodeFooterVisibleToggleButton_Click(object sender, EventArgs e)
        {
            if(_currentTreeNode!=null && _currentTreeNode.Footer!=null)
                _currentTreeNode.Footer.Visible= (!_currentTreeNode.Footer.Visible);
        }

        private void activeContentVisibleToggleButton_Click(object sender, EventArgs e)
        {
            if(_currentContentNode!=null)
                _currentContentNode.Visible= (!_currentContentNode.Visible);
        }

        private void activeTreeNodeToggleVisibleButton_Click(object sender, EventArgs e)
        {
            if(_currentTreeNode!=null)
                _currentTreeNode.Visible= (!_currentTreeNode.Visible);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (_currentTreeNode == null)
                return;
            
            _treeNodesCount ++;
            GroupItemNode nn = new GroupItemNode {Text= "Header " + _treeNodesCount , Height = 30};
            _currentTreeNode.AddTreeNode(nn);
        }

        private void rootNodeActiveTreeNodeButton1_Click(object sender, EventArgs e)
        {
            _currentTreeNode = _treeVFlowControl1.RootNode;
            lblTreeNode.Text = _currentTreeNode.Text;
        }
    }
}
