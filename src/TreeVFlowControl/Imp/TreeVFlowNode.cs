using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using TreeVFlowControl.Core;

namespace TreeVFlowControl.Imp
{
   
    public class TreeVFlowNode: TableLayoutPanel, IGraphicalTreeNode
    {
        private int _suspendLayoutCount;
        public int LevelIndent { get; set; } = 10;
        private Control _header;
        private Control _footer;
        private bool _isFold;
        
        
        public TreeVFlowNode()
        : this(null) { }
        
        public TreeVFlowNode(Control header, Control footer=null)
        {
            ColumnCount = 1;
            //FlowDirection = FlowDirection.TopDown;
            DoubleBuffered = true;
            Header = header;
            Footer = footer;            
        }
        
        public Control Header
        {
            get=> _header;
            set
            {
                if (_header != null)
                {
                    Controls.RemoveAt(0);
                    _header.Click -= HeaderClick;
                    _header.DoubleClick -= HeaderDoubleClick;
                    _header.SizeChanged -= ControlResize;
                    _header.VisibleChanged -= ControlResize;
                }

                if (value != null)
                {
                    Controls.Add(value);
                    Controls.SetChildIndex(value, 0);
                    
                    value.Click += HeaderClick;
                    value.DoubleClick += HeaderDoubleClick;
                    value.SizeChanged += ControlResize;
                    value.VisibleChanged += ControlResize;
                }
                
                _header = value;
                
                RefreshHeight();
            }
        }

        private void ControlResize(object sender, EventArgs e)
        {
            RefreshHeight();
        }
        
        public Control Footer
        {
            get => _footer;
            set
            {
                if (_footer != null)
                {
                    Controls.RemoveAt(Controls.Count - 1);
                    _footer.Click -= FooterClick;
                    _footer.DoubleClick -= FooterDoubleClick;
                    _footer.SizeChanged -= ControlResize;
                    _footer.VisibleChanged -= ControlResize;
                }

                if (value != null)
                {
                    value.Left = LevelIndent*TreeLevel;
                    value.Width = Width;
                    Controls.Add(value);
                    Controls.SetChildIndex(value, Controls.Count-1);
                    value.Click += FooterClick;
                    value.DoubleClick += FooterDoubleClick;
                    value.SizeChanged += ControlResize;
                    value.VisibleChanged += ControlResize;
                }

                _footer = value;
                
                RefreshHeight();
            }
        }

        public IGraphicalTreeNode ParentTreeNode => Parent as TreeVFlowNode;
        public IGraphicalTreeNode RootTreeNode=>this.ParentTreeNode==null?this:this.ParentTreeNode.RootTreeNode;
        public int TreeLevel => this.ParentTreeNode == null ? 0 : this.ParentTreeNode.TreeLevel + 1;

        
        private void HeaderClick(object sender, EventArgs args)
        {
            OnTreeNodeHeaderClick(new TreeNodeEventArgs(this, null));
        }
        private void HeaderDoubleClick(object sender, EventArgs args)
        {
            OnTreeNodeHeaderDoubleClick(new TreeNodeEventArgs(this, null));
        }
        private void FooterClick(object sender, EventArgs args)
        {
            OnTreeNodeFooterClick(new TreeNodeEventArgs(this, null));
        }
        private void FooterDoubleClick(object sender, EventArgs args)
        {
            OnTreeNodeFooterDoubleClick(new TreeNodeEventArgs(this, null));
        }
        private void ContentClick(object sender, EventArgs args)
        {
            OnContentNodeClick(new TreeNodeEventArgs(this, (Control)sender));
        }
        private void ContentDoubleClick(object sender, EventArgs args)
        {
            OnContentNodeDoubleClick(new TreeNodeEventArgs(this, (Control)sender));
        }

#region Events
        public event TreeNodeEventHandler TreeNodeHeaderClick;
        public event TreeNodeEventHandler TreeNodeHeaderDoubleClick;
        public event TreeNodeEventHandler TreeNodeFooterClick;
        public event TreeNodeEventHandler TreeNodeFooterDoubleClick;
        public event TreeNodeEventHandler ContentNodeClick;
        public event TreeNodeEventHandler ContentNodeDoubleClick;
        public event TreeNodeEventHandler TreeNodeFold;
        public event TreeNodeEventHandler TreeNodeUnfold;
        public event TreeNodeEventHandler TreeNodeAdded;
        public event TreeNodeEventHandler TreeNodeRemoved;
        public event TreeNodeEventHandler ContentNodeAdded;
        public event TreeNodeEventHandler ContentNodeRemoved;
        public event TreeNodeEventHandler TreeNodeRefresh;
        
        protected virtual void OnTreeNodeHeaderClick(TreeNodeEventArgs args)
        {
            TreeNodeHeaderClick?.Invoke(this, args);
        }
        protected virtual void OnTreeNodeHeaderDoubleClick(TreeNodeEventArgs args)
        {
            TreeNodeHeaderDoubleClick?.Invoke(this, args);
        }
        protected virtual void OnTreeNodeFooterClick(TreeNodeEventArgs args)
        {
            TreeNodeFooterClick?.Invoke(this, args);
        }
        protected virtual void OnTreeNodeFooterDoubleClick(TreeNodeEventArgs args)
        {
            TreeNodeFooterDoubleClick?.Invoke(this, args);
        }      
        protected virtual void OnTreeNodeFold(TreeNodeEventArgs args)
        {
            TreeNodeFold?.Invoke(this, args);
        }
        protected virtual void OnTreeNodeUnfold(TreeNodeEventArgs args)
        {
            TreeNodeUnfold?.Invoke(this, args);
        }
        protected virtual void OnTreeNodeAdded(TreeNodeEventArgs args)
        {
            TreeNodeAdded?.Invoke(this, args);
        }
        protected virtual void OnTreeNodeRemoved(TreeNodeEventArgs args)
        {
            TreeNodeRemoved?.Invoke(this, args);
        }
        protected virtual void OnTreeNodeRefresh(TreeNodeEventArgs args)
        {
            TreeNodeRefresh?.Invoke(this, args);
        }
        protected virtual void OnContentNodeClick(TreeNodeEventArgs args)
        {
            ContentNodeClick?.Invoke(this, args);
        }
        protected virtual void OnContentNodeDoubleClick(TreeNodeEventArgs args)
        {
            ContentNodeDoubleClick?.Invoke(this, args);
        }
        protected virtual void OnContentNodeAdded(TreeNodeEventArgs args)
        {
            ContentNodeAdded?.Invoke(this, args);
        }
        protected virtual void OnContentNodeRemoved(TreeNodeEventArgs args)
        {
            ContentNodeRemoved?.Invoke(this, args);
        }

#endregion
        
        public void AddContent(Control content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            if (TreeNodes.Count > 0) return;
            
            content.Left = LevelIndent*TreeLevel;
            content.Width = Width;
            Controls.Add(content);
            Controls.SetChildIndex(content, Controls.Count - (_footer==null?1:2));
            content.Click += ContentClick;
            content.DoubleClick += ContentDoubleClick;
            content.SizeChanged += ControlResize;
            content.VisibleChanged += ControlResize;
            RefreshHeight();
            OnContentNodeAdded(new TreeNodeEventArgs(this, content));
        }

        public void RemoveContent(Control content)
        {
            Controls.Remove(content);
            content.Click -= ContentClick;
            content.DoubleClick -= ContentDoubleClick;
            content.SizeChanged -= ControlResize;
            content.VisibleChanged -= ControlResize;
            RefreshHeight();
            
            OnContentNodeRemoved(new TreeNodeEventArgs(this, content));
        }
        
        public IList<Control> TreeContent=>Controls
            .Cast<Control>()
            .Where(v=>!(v is TreeVFlowNode || v==Header || v==Footer))
            .ToList();
        
        public IList<IGraphicalTreeNode> TreeNodes=>Controls
            .Cast<Control>()
            .OfType<IGraphicalTreeNode>()
            .ToList();

        public IGraphicalTreeNode AddTreeNode()
        {
            return AddTreeNode(new TreeVFlowNode());
        }
        
        public IGraphicalTreeNode AddTreeNode(IGraphicalTreeNode newTreeNode)
        {
            var newTreeVFlowNode = newTreeNode as TreeVFlowNode; 
            if(newTreeVFlowNode==null) throw new ArgumentNullException(nameof(newTreeNode));
            
            SuspendLayoutTree();
            newTreeVFlowNode.Visible = true;
            newTreeVFlowNode.LevelIndent = LevelIndent;
            //newTreeNode.FlowDirection = FlowDirection;
            //newTreeNode.WrapContents = WrapContents;
            newTreeVFlowNode.ColumnCount = ColumnCount;
            newTreeVFlowNode.AutoSize = false;
            newTreeVFlowNode.Width = Width;
            Controls.Add(newTreeVFlowNode);
            Controls.SetChildIndex(newTreeVFlowNode, Controls.Count - (_footer==null?1:2));
            ResumeLayoutTree();
            
            OnTreeNodeAdded(new TreeNodeEventArgs(newTreeVFlowNode, null));
            newTreeVFlowNode.RefreshHeight();

            
            return newTreeVFlowNode;
        }
        
        public void RemoveTreeNode(IGraphicalTreeNode treeNodeToRemove)
        {
            var treeNodeToRemoveToRemove = treeNodeToRemove as TreeVFlowNode; 
            if(treeNodeToRemoveToRemove==null) throw new ArgumentNullException(nameof(treeNodeToRemove));
            
            RootTreeNode.SuspendLayoutTree();
            Controls.Remove(treeNodeToRemoveToRemove);
            RefreshHeight();
            RootTreeNode.ResumeLayoutTree();
            
            OnTreeNodeRemoved(new TreeNodeEventArgs(treeNodeToRemoveToRemove, null));
        }
        public void RemoveTreeNode()
        {
            ParentTreeNode?.RemoveTreeNode(this);
        }
        
        public bool IsFold=>_isFold;
        public void TogleFold()
        {
            Fold(!_isFold);
        }
        
        public void Fold() {
            Fold(true);
        }
        
        public void Unfold() {
            Fold(false);
        }
        private void Fold(bool fold) {
            if (fold==_isFold) return;
            
            _isFold = fold;

            RootTreeNode.SuspendLayoutTree();
            var controls = Controls.Cast<Control>().ToList();
            for (int i = 0; i < controls.Count; i++)
                if (i > (_header==null?-1:0) && i < controls.Count - (_footer==null?0:1)) controls[i].Visible = !fold;
            
            //RefreshHeight();

            RootTreeNode.ResumeLayoutTree();
            
            if (fold)
                OnTreeNodeFold(new TreeNodeEventArgs(this, null));
            else
                OnTreeNodeUnfold(new TreeNodeEventArgs(this, null));
        }

        protected override void OnTextChanged(EventArgs e)
        {
            OnTreeNodeRefresh(new TreeNodeEventArgs(this, null));
        }
        
        private int ControlHeight(Control control)
        {
            if(control==null) return 0;
            return control.Visible?control.Height:0;
        }

        private int lastWidth;
        protected override void OnClientSizeChanged(EventArgs e)
        {
            if (lastWidth != Width)
            {
                WidthChanged();
                lastWidth = Width;
            }

            base.OnClientSizeChanged(e);
        }
        
        private void WidthChanged()
        {
            RootTreeNode.SuspendLayoutTree();
            var mMargin = Margin;
            mMargin.Left = (TreeLevel * LevelIndent) + DefaultMargin.Left;
            Margin = mMargin;
            Controls.Cast<Control>()
                .ToList().ForEach(v => v.Width = Width - Margin.Horizontal - 6);
            RootTreeNode.ResumeLayoutTree();
        }

        public void SuspendLayoutTree()
        {
            SuspendLayoutTreeNode();;
            TreeNodes.ToList().ForEach(v=>v.SuspendLayoutTreeNode());
        }
        public void SuspendLayoutTreeNode()
        {
            if (_suspendLayoutCount == 0)
            {
                Debug.WriteLine($"SuspendLayoutTreeNode ({this}) do it!");
                SuspendLayout();
                Controls.Cast<Control>().ToList().ForEach(v => v.SuspendLayout());
            }

            _suspendLayoutCount++;
        }
        
        public void ResumeLayoutTree()
        {
            ResumeLayoutTreeNode();
            TreeNodes.ToList().ForEach(v=>v.ResumeLayoutTreeNode());
        }
        public void ResumeLayoutTreeNode()
        {
            if (_suspendLayoutCount > 0)
            {
                _suspendLayoutCount--;

                if (_suspendLayoutCount == 0)
                {
                    Debug.WriteLine($"ResumeLayoutTreeNode ({this}) do it!!");
                    ResumeLayout();
                    Controls.Cast<Control>().ToList().ForEach(v => v.ResumeLayout());
                }
            }
        }
        
        private void RefreshHeight()
        {
            RootTreeNode.SuspendLayoutTree();
            Height = (_isFold?ControlHeight(_header):
                Controls.Cast<Control>()
                    .ToList()
                    .Sum(v => ControlHeight(v) + v.Margin.Vertical));
            
            ((TreeVFlowNode)ParentTreeNode)?.RefreshHeight();
            
            RootTreeNode.ResumeLayoutTree();
        }

        public override string ToString()
        {
            return Text;
        }
        
        public IGraphicalTreeNode TreeDeepFirstOrDefault(Func<IGraphicalTreeNode, bool> predicate)
        {
            if(predicate(this)) return this;
            foreach (IGraphicalTreeNode treeNode in TreeNodes)
            {
                IGraphicalTreeNode ret = treeNode.TreeDeepFirstOrDefault(predicate);
                if (ret != null) return ret;
            }

            return null;
        }
        
        public IEnumerable<IGraphicalTreeNode> TreeDeepWhere(Func<IGraphicalTreeNode, bool> predicate)
        {
            var ret = new List<IGraphicalTreeNode>();
            TreeDeepWhere(predicate, ret);
            return ret;
        }
        
        private void TreeDeepWhere(Func<IGraphicalTreeNode, bool> predicate, List<IGraphicalTreeNode> ret)
        {
            ret.AddRange(TreeNodes.Where(predicate));
            foreach (IGraphicalTreeNode treeNode in TreeNodes)
                ((TreeVFlowNode)treeNode).TreeDeepWhere(predicate, ret);
        }
        
        public Control ContentDeepFirstOrDefault(Func<Control, bool> predicate)
        {
            Control ret=TreeContent.FirstOrDefault(predicate);
            if(ret!=null) return ret;
            foreach (IGraphicalTreeNode treeNode in TreeNodes)
                return treeNode.ContentDeepFirstOrDefault(predicate);

            return null;
        }
        
        public IEnumerable<Control> ContentDeepWhere(Func<Control, bool> predicate)
        {
            var ret = new List<Control>();
            ContentDeepWhere(predicate, ret);
            return ret;
        }
        
        private void ContentDeepWhere(Func<Control, bool> predicate, List<Control> ret)
        {
            ret.AddRange(TreeContent.Where(predicate));
            foreach (IGraphicalTreeNode treeNode in TreeNodes)
                ((TreeVFlowNode)treeNode).ContentDeepWhere(predicate, ret);
        }
    }
}