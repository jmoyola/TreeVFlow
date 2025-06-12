using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TreeVFlowControl.Core;
using TreeVFlowControl.Imp;

namespace TreeVFlowWFormTest;

public class GroupItemNode:TreeVFlowNode
{
    private int _contentPageSize = 5;
    private int _contentPage = 1;
    private int _maxPaginatedContent;
    private void RecalculateMaxPaginatedContent()
    {
        _maxPaginatedContent=_contentPageSize > 0 ? _contentPageSize * _contentPage : Int32.MaxValue;
    }

    public int ContentPageSize
    {
        get => _contentPageSize;
        set
        {
            _contentPageSize = value;
            RecalculateMaxPaginatedContent();
        }
    }

    public int ContentPage
    {
        get => _contentPage;
        set
        {
            _contentPage = value > 0 ? value : throw new ArgumentOutOfRangeException(nameof(value));
            RecalculateMaxPaginatedContent();
        }
    }

    public int MaxPaginatedContent => _maxPaginatedContent;

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
        RecalculateMaxPaginatedContent();
        
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
        
        Footer = new Label(){Height = 30, Text = "Show More", Visible = false};
        //Footer.Click +=(_,_)=> ShowMoreClick();
        
    }

    protected override void OnBeforeContentNodeAdded(CancellableEventArgs<TreeNodeEventArgs> args)
    {
        base.OnBeforeContentNodeAdded(args);

        if (args.Cancel) return;

        if (_contentPageSize>0 && TreeContent.Count(v => v.Visible) >= MaxPaginatedContent)
        {
            args.Args.Content.Visible = false;
            Footer.Visible = true;
        }
        else
        {
            Footer.Visible = false;
        }
    }
    
    private void RefreshPaging()
    {
        if (_contentPageSize > 0 && TreeContent.Count(v => v.Visible) > MaxPaginatedContent)
        {
            TreeContent.Take(MaxPaginatedContent).ToList().ForEachIndex((i, v) => v.Visible = i < MaxPaginatedContent);
        }
    }

    protected override void OnTreeNodeFooterClick(TreeNodeEventArgs args)
    {
        if(_contentPageSize>0)
        {
            _contentPage++;
        
            RefreshPaging();
            Footer.Visible = false;
        }
        
        base.OnTreeNodeFooterClick(args);
    }

    private void ShowMoreClick()
    {
        RefreshPaging();
        
        
    }

    protected override void OnAfterTreeNodeExpanded(TreeNodeEventArgs args)
    {
        ((Button)Header).Image = ArrowUpImage;
    }

    protected override void OnAfterTreeNodeCollapsed(TreeNodeEventArgs args)
    {
        ((Button)Header).Image = ArrowDownImage;
    }
}