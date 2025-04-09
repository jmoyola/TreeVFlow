using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using HierarchicalTree.Properties;
using TreeVFlowControl.Core;
using TreeVFlowControl.Imp;

namespace TreeVFlowWFormTest;

public class GroupItemNode:TreeVFlowNode
{
    protected static Bitmap _arrowUpImage;
    protected static Bitmap _arrowDownImage;
    protected static Bitmap _padLockImage;

    static GroupItemNode()
    {
        _arrowUpImage = new Bitmap(Image.FromStream(
            Assembly.GetCallingAssembly().GetManifestResourceStream("TreeVFlowWFormTest.Resources.arrow_up_32.png")));
        _arrowDownImage = new Bitmap(Image.FromStream(
                Assembly.GetCallingAssembly().GetManifestResourceStream("TreeVFlowWFormTest.Resources.arrow_down_32.png")));
        _padLockImage = new Bitmap(Image.FromStream(
                Assembly.GetCallingAssembly().GetManifestResourceStream("TreeVFlowWFormTest.Resources.pad_lock_32.png")));
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
        btn.Image = _arrowUpImage;
        btn.ImageAlign = ContentAlignment.MiddleLeft;
        
        this.Footer = new Label(){Height = 30, Text = "Show More", Visible = false};
    }

    protected override void OnTreeNodeExpanded(TreeNodeEventArgs args)
    {
        ((Button)Header).Image = _arrowUpImage;
    }

    protected override void OnTreeNodeCollapsed(TreeNodeEventArgs args)
    {
        ((Button)Header).Image = _arrowDownImage;
    }

    public override void DisableItem()
    {
        base.DisableItem();
    }

    public override void EnableItem()
    {
        base.EnableItem();
    }
}