using System;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using HierarchicalTree.Properties;
using TreeVFlowControl.Core;
using TreeVFlowControl.Imp;

namespace TreeVFlowWFormTest;

public class GroupItemNode:TreeVFlowNode
{
    protected readonly static Bitmap ArrowUpImage;
    protected readonly static Bitmap ArrowDownImage;
    protected readonly static Bitmap PadLockImage;

    static GroupItemNode()
    {
        Assembly ass = Assembly.GetAssembly(typeof(GroupItemNode));
        ArrowUpImage = new Bitmap(Image.FromStream(
            ass.GetManifestResourceStream("TreeVFlowWFormTest.Resources.arrow_up_32.png") ?? throw new InvalidOperationException()));
        ArrowDownImage = new Bitmap(Image.FromStream(
                ass.GetManifestResourceStream("TreeVFlowWFormTest.Resources.arrow_down_32.png") ?? throw new InvalidOperationException()));
        PadLockImage = new Bitmap(Image.FromStream(
                ass.GetManifestResourceStream("TreeVFlowWFormTest.Resources.pad_lock_32.png") ?? throw new InvalidOperationException()));
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
        
        btn.Click +=(_,_)=> ToggleItems();
        TextChanged +=(_,_)=> btn.Text=Text;
        btn.Image = ArrowUpImage;
        btn.ImageAlign = ContentAlignment.MiddleLeft;
        
        this.Footer = new Label(){Height = 30, Text = "Show More", Visible = false};
    }

    protected override void OnTreeNodeExpanded(TreeNodeEventArgs args)
    {
        ((Button)Header).Image = ArrowUpImage;
    }

    protected override void OnTreeNodeCollapsed(TreeNodeEventArgs args)
    {
        ((Button)Header).Image = ArrowDownImage;
    }
}