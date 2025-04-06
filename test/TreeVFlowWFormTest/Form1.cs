using System;

using System.Drawing;
using System.Globalization;

using System.Threading;
using System.Windows.Forms;
using TreeVFlowControl.Core;
using TreeVFlowControl.Imp;


namespace TreeVFlowWFormTest
{
    public partial class Form1 : Form
    {
        private int _count = 0;
        private TreeVFlowNode _currentTreeNode;
        private Control _currentContentNode;
        
        public Form1()
        {
            InitializeComponent();
            //this._treeVFlowNode1.WrapContents = true;
            this._treeVFlowNode1.AutoSize = false;
            //this._treeVFlowNode1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            
            
            _currentTreeNode = _treeVFlowNode1;
            JoinAllEvents(this._treeVFlowNode1);
        }

        private void JoinAllEvents(TreeVFlowNode node)
        {
            node.TreeNodeSelected += (s, a) => _currentTreeNode = a.TreeNode as TreeVFlowNode;
            node.ContentNodeSelected += (s, args) => _currentContentNode = args.Content;
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
            var p=_currentContentNode as Control;
            (p?.Parent as TreeVFlowNode)?.RemoveContent(p);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;

            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("zh-TW");
            Thread.CurrentThread.CurrentCulture=CultureInfo.GetCultureInfo("zh-TW");
            label1.TextAlign = ContentAlignment.MiddleRight;

            label1.Font = new System.Drawing.Font("Microsoft JhengHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Text = "卡斯亞卡\r -7.5";
            
            Thread.CurrentThread.CurrentCulture=ci;
        }
    }
}
