using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using TreeVFlowControl.Core;
using TreeVFlowControl.Imp;

namespace TreeVFlowWFormTest;

public class GroupItemNode:TreeVFlowNode
{
    private static readonly Bitmap ArrowUpImage = GetResourceBitMap("TreeVFlowWFormTest.Resources.arrow_up_32.png");
    private static readonly Bitmap ArrowDownImage = GetResourceBitMap("TreeVFlowWFormTest.Resources.arrow_down_32.png");
    private static readonly Bitmap PadLockImage=GetResourceBitMap("TreeVFlowWFormTest.Resources.pad_lock_32.png");

    private static Bitmap GetResourceBitMap(string resourcePath)
    {
        Assembly ass = Assembly.GetAssembly(typeof(GroupItemNode));
        return new Bitmap(Image.FromStream(
            ass.GetManifestResourceStream(resourcePath) ?? throw new InvalidOperationException()));
    }
    
    public GroupItemNode()
    {
        Init();
    }

    private void Init()
    {
        Button btn = new Button()
        {
            FlatStyle = FlatStyle.Flat,
            Height = 30
        };
        btn.FlatAppearance.BorderSize = 1;
        
        Header = btn;
        
        btn.Click +=(_,_)=> Expand=(!Expand);
        TextChanged +=(_,_)=> btn.Text=Text;
        btn.Image = ArrowUpImage;
        btn.ImageAlign = ContentAlignment.MiddleLeft;
        
        this.Footer = new Label(){Height = 30, Text = "Show More", Visible = false};
    }

    protected override void OnTreeNodeExpandedChanged(TreeNodeEventArgs args)
    {
        ((Button)Header).Image =args.TreeNode.Expand? ArrowUpImage:ArrowDownImage;
    }

}