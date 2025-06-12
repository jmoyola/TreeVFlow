using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TreeVFlowControl.Core;

namespace TreeVFlowControl.Imp
{
    public class TreeVFlowNode: TableLayoutPanel, IGraphicalTreeNode
    {
        private readonly SemaphoreSlim _nodeLock = new (1, 1);
        
        public int LevelIndent { get; set; } = 10;
        private Control _header;
        private Control _footer;
        private bool _isExpanded;
        private bool _isDisabled;
        
        public TreeVFlowNode()
        : this(null) { }
        
        public TreeVFlowNode(Control header, Control footer=null)
        {
            Init();
            Header = header;
            Footer = footer; 
        }

        private void Init()
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ColumnCount = 1;
            DoubleBuffered = true;
        }
        
        private SemaphoreSlim TreeNodeLock=> ((TreeVFlowNode)RootTreeNode)._nodeLock;
        
        public Control Header
        {
            get=> _header;
            set
            {
                try
                {
                    TreeNodeLock.Wait();

                    SuspendLayout();
                    if (_header != null)
                    {
                        Controls.RemoveAt(0);
                        SubControlRemoved(_header);
                    }

                    if (value != null)
                    {
                        Controls.Add(value);
                        Controls.SetChildIndex(value, 0);
                        SubControlAdded(value);
                    }
                    
                    _header = value;
                }
                finally
                {
                    ResumeLayout();
                    TreeNodeLock.Release();
                }
            }
        }
        
        public Control Footer
        {
            get => _footer;
            set
            {
                try
                {
                    TreeNodeLock.Wait();
                    
                    SuspendLayout();
                    if (_footer != null)
                    {
                        Controls.RemoveAt(Controls.Count - 1);
                        SubControlRemoved(_footer);
                    }

                    if (value != null)
                    {
                        Controls.Add(value);
                        Controls.SetChildIndex(value, Controls.Count - 1);
                        SubControlAdded(value);
                    }

                    _footer = value;
                }
                finally
                {
                    ResumeLayout();
                    TreeNodeLock.Release();
                }
            }
        }

        public IGraphicalTreeNode ParentTreeNode => Parent as TreeVFlowNode;
        public IGraphicalTreeNode RootTreeNode=>ParentTreeNode==null?this:ParentTreeNode.RootTreeNode;
        public Panel PanelContainer
        {
            get
            {
                Panel pnl = Parent as Panel;
                return pnl ?? ParentTreeNode.PanelContainer;
            }
        }

        public int TreeLevel => ParentTreeNode == null ? 0 : ParentTreeNode.TreeLevel + 1;
        
        private void SubControl_Click(object sender, EventArgs args)
        {
            if (sender == _header)
                OnTreeNodeHeaderClick(new TreeNodeEventArgs(this, (Control)sender));
            else if (sender == _footer)
                OnTreeNodeFooterClick(new TreeNodeEventArgs(this, (Control)sender));
            else
                OnContentNodeClick(new TreeNodeEventArgs(this, (Control)sender));

        }
        private void SubControl_DoubleClick(object sender, EventArgs args)
        {
            if (sender == _header)
                OnTreeNodeHeaderDoubleClick(new TreeNodeEventArgs(this, (Control)sender));
            else if (sender == _footer)
                OnTreeNodeFooterDoubleClick(new TreeNodeEventArgs(this, (Control)sender));
            else
                OnContentNodeDoubleClick(new TreeNodeEventArgs(this, (Control)sender));
        }

#region Events
        public event TreeNodeEventHandler TreeNodeHeaderClick;
        public event TreeNodeEventHandler TreeNodeHeaderDoubleClick;
        public event TreeNodeEventHandler TreeNodeFooterClick;
        public event TreeNodeEventHandler TreeNodeFooterDoubleClick;
        public event TreeNodeEventHandler ContentNodeClick;
        public event TreeNodeEventHandler ContentNodeDoubleClick;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeCollapsed;
        public event TreeNodeEventHandler AfterTreeNodeCollapsed;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeExpanded;
        public event TreeNodeEventHandler AfterTreeNodeExpanded;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeAdded;
        public event TreeNodeEventHandler AfterTreeNodeAdded;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeRemoved;
        public event TreeNodeEventHandler AfterTreeNodeRemoved;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeContentNodeAdded;
        public event TreeNodeEventHandler AfterContentNodeAdded;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeContentNodeRemoved;
        public event TreeNodeEventHandler AfterContentNodeRemoved;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeDisabled;
        public event TreeNodeEventHandler AfterTreeNodeDisabled;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeEnabled;
        public event TreeNodeEventHandler AfterTreeNodeEnabled;

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
        protected virtual void OnBeforeTreeNodeExpanded(CancellableEventArgs<TreeNodeEventArgs> args)
        {
            BeforeTreeNodeExpanded?.Invoke(this, args);
        }
        protected virtual void OnAfterTreeNodeExpanded(TreeNodeEventArgs args)
        {
            AfterTreeNodeExpanded?.Invoke(this, args);
        }
        protected virtual void OnBeforeTreeNodeCollapsed(CancellableEventArgs<TreeNodeEventArgs> args)
        {
            BeforeTreeNodeCollapsed?.Invoke(this, args);
        }
        protected virtual void OnAfterTreeNodeCollapsed(TreeNodeEventArgs args)
        {
            AfterTreeNodeCollapsed?.Invoke(this, args);
        }
        protected virtual void OnBeforeTreeNodeAdded(CancellableEventArgs<TreeNodeEventArgs> args)
        {
            BeforeTreeNodeAdded?.Invoke(this, args);
        }
        protected virtual void OnAfterTreeNodeAdded(TreeNodeEventArgs args)
        {
            AfterTreeNodeAdded?.Invoke(this, args);
        }
        protected virtual void OnBeforeTreeNodeRemoved(CancellableEventArgs<TreeNodeEventArgs> args)
        {
            BeforeTreeNodeRemoved?.Invoke(this, args);
        }
        protected virtual void OnAfterTreeNodeRemoved(TreeNodeEventArgs args)
        {
            AfterTreeNodeRemoved?.Invoke(this, args);
        }
        protected virtual void OnBeforeContentNodeAdded(CancellableEventArgs<TreeNodeEventArgs>  args)
        {
            BeforeContentNodeAdded?.Invoke(this, args);
        }
        protected virtual void OnAfterContentNodeAdded(TreeNodeEventArgs args)
        {
            AfterContentNodeAdded?.Invoke(this, args);
        }
        protected virtual void OnBeforeContentNodeRemoved(CancellableEventArgs<TreeNodeEventArgs> args)
        {
            BeforeContentNodeRemoved?.Invoke(this, args);
        }
        protected virtual void OnAfterContentNodeRemoved(TreeNodeEventArgs args)
        {
            AfterContentNodeRemoved?.Invoke(this, args);
        }
        protected virtual void OnBeforeTreeNodeDisabled(CancellableEventArgs<TreeNodeEventArgs> args)
        {
            BeforeTreeNodeDisabled?.Invoke(this, args);
        }
        protected virtual void OnAfterTreeNodeDisabled(TreeNodeEventArgs args)
        {
            AfterTreeNodeDisabled?.Invoke(this, args);
        }
        protected virtual void OnBeforeTreeNodeEnabled(CancellableEventArgs<TreeNodeEventArgs> args)
        {
            BeforeTreeNodeEnabled?.Invoke(this, args);
        }
        protected virtual void OnAfterTreeNodeEnabled(TreeNodeEventArgs args)
        {
            AfterTreeNodeEnabled?.Invoke(this, args);
        }
#endregion

        private void SubControlAdded(Control control)
        {
            control.Left = LevelIndent * TreeLevel;
            control.Width = Width;
            control.Click += SubControl_Click;
            control.DoubleClick += SubControl_DoubleClick;
        }
        
        private void SubControlRemoved(Control control)
        {
            control.Click -= SubControl_Click;
            control.DoubleClick -= SubControl_DoubleClick;
        }
        
        public IList<Control> TreeContent=>GetTreeContent();
        private IList<Control> GetTreeContent()=>Controls
            .Cast<Control>()
            .Where(v=>!(v is TreeVFlowNode || v==Header || v==Footer))
            .ToList();
        
        public IList<IGraphicalTreeNode> TreeNodes=>GetTreeNodes();

        private IList<IGraphicalTreeNode> GetTreeNodes()=>Controls
            .Cast<Control>()
            .OfType<IGraphicalTreeNode>()
            .ToList();
        public IGraphicalTreeNode AddTreeNode()
        {
            return AddTreeNode(new TreeVFlowNode());
        }
        
        public void AddContent(Control content)
        {
            try
            {
                TreeNodeLock.Wait();
                SuspendLayout();
                
                var cb = new CancellableEventArgs<TreeNodeEventArgs>(new TreeNodeEventArgs(this, content));
                OnBeforeContentNodeAdded(cb);
                if (cb.Cancel) return;
                
                if (content == null) throw new ArgumentNullException(nameof(content));
                if (TreeNodes.Count > 0) return;
                
                content.SuspendLayout();
                
                content.Width = this.Width - this.Margin.Horizontal*2;
                Controls.Add(content);
                Controls.SetChildIndex(content, Controls.Count - (_footer == null ? 1 : 2));
                
                SubControlAdded(content);
                
                content.ResumeLayout();
                
                OnAfterContentNodeAdded(new TreeNodeEventArgs(this, content));
            }
            finally
            {
                ResumeLayout();
                TreeNodeLock.Release();
            }
        }

        public void RemoveContent(Control content)
        {
            try
            {
                TreeNodeLock.Wait();
                SuspendLayout();
                
                var cb = new CancellableEventArgs<TreeNodeEventArgs>(new TreeNodeEventArgs(this, content));
                OnBeforeContentNodeRemoved(cb);
                if (cb.Cancel) return;
                
                content.SuspendLayout();
                
                Controls.Remove(content);
                SubControlRemoved(content);

                content.ResumeLayout();
                
                
                OnAfterContentNodeRemoved(new TreeNodeEventArgs(this, content));
            }
            finally
            {
                ResumeLayout();
                TreeNodeLock.Release();
            }
        }
        
        public void ClearContentNodes()
        {
            try
            {
                TreeNodeLock.Wait();
                
                SuspendLayout();
                TreeContent.ToList().ForEach(v =>
                {
                    v.SuspendLayout();
                
                    Controls.Remove(v);
                    SubControlRemoved(v);

                    v.ResumeLayout();
                });
                
            }
            finally
            {
                ResumeLayout();
                TreeNodeLock.Release();
            }
        }
        
        public IGraphicalTreeNode AddTreeNode(IGraphicalTreeNode newTreeNode)
        {
            try
            {
                TreeNodeLock.Wait();
                SuspendLayout();
                
                var newTreeVFlowNode = newTreeNode as TreeVFlowNode;
                if (newTreeVFlowNode == null) throw new ArgumentNullException(nameof(newTreeNode));
                
                newTreeVFlowNode.Visible = true;
                var cb = new CancellableEventArgs<TreeNodeEventArgs>(new TreeNodeEventArgs(newTreeVFlowNode));
                OnBeforeTreeNodeAdded(cb);
                if (cb.Cancel) return newTreeVFlowNode;
                
                newTreeVFlowNode.SuspendLayout();
                
                newTreeVFlowNode.LevelIndent = LevelIndent;
                newTreeVFlowNode.ColumnCount = ColumnCount;
                Controls.Add(newTreeVFlowNode);
                Controls.SetChildIndex(newTreeVFlowNode, Controls.Count - (_footer == null ? 1 : 2));
                
                newTreeVFlowNode.RefreshNodeLayout();
                RefreshNodeLayout();
                
                newTreeVFlowNode.ResumeLayout();
                
                
                OnAfterTreeNodeAdded(new TreeNodeEventArgs(newTreeVFlowNode));
                return newTreeVFlowNode;
            }
            finally
            {
                ResumeLayout();
                TreeNodeLock.Release();
            }
        }
        
        public void RemoveTreeNode(IGraphicalTreeNode treeNodeToRemove)
        {
            try
            {
                TreeNodeLock.Wait();
                SuspendLayout();
                
                var treeNodeToRemoveToRemove = treeNodeToRemove as TreeVFlowNode;
                if (treeNodeToRemoveToRemove == null) throw new ArgumentNullException(nameof(treeNodeToRemove));

                var cb = new CancellableEventArgs<TreeNodeEventArgs>(new TreeNodeEventArgs(treeNodeToRemoveToRemove));
                OnBeforeTreeNodeRemoved(cb);
                if (cb.Cancel) return;

                Controls.Remove(treeNodeToRemoveToRemove);
                
                
                OnAfterTreeNodeRemoved(new TreeNodeEventArgs(treeNodeToRemoveToRemove));
            }
            finally
            {
                ResumeLayout();
                TreeNodeLock.Release();
            }
        }
        
        public void ClearTreeNodes()
        {
            try
            {
                TreeNodeLock.Wait();
                
                SuspendLayout();

                TreeNodes.ToList().ForEach(v =>
                {
                    ((TreeVFlowNode)v).SuspendLayout();
                    Controls.Remove((TreeVFlowNode)v);
                    ((TreeVFlowNode)v).ResumeLayout();
                });
                
                ResumeLayout();
            }
            finally
            {
                TreeNodeLock.Release();
            }
        }
        
        public void ClearAll()
        {
            try
            {
                TreeNodeLock.Wait();
                
                ClearTreeNodes();
                ClearContentNodes();
            }
            finally
            {
                TreeNodeLock.Release();
            }
        }
        
        public virtual bool IsDisabled=>_isDisabled;

        public virtual void DisableTreeNode()
        {
            DisableTreeNode(true);
        }

        public virtual void EnableTreeNode()
        {
            DisableTreeNode(false);
        }
        
        private void DisableTreeNode(bool disable)
        {
            if (_isDisabled==disable) return;

            var bc = new CancellableEventArgs<TreeNodeEventArgs>(new TreeNodeEventArgs(this));
            if(disable)
                OnBeforeTreeNodeDisabled(bc);
            else
                OnBeforeTreeNodeEnabled(bc);
            if (bc.Cancel) return;
            
            Controls.Cast<Control>().ToList().ForEach(v=>v.Enabled=!disable);
            _isDisabled = disable;
            
            if(disable)
                OnAfterTreeNodeDisabled(new TreeNodeEventArgs(this));
            else
                OnAfterTreeNodeEnabled(new TreeNodeEventArgs(this));
        }
        
        public bool IsExpanded=>_isExpanded;
        public void ToggleItems()
        {
            Expand(!_isExpanded);
        }
        
        public void Collapse() {
            Expand(false);
        }
        
        public void Expand() {
            Expand(true);
        }
        private void Expand(bool expand) {
            try
            {
                TreeNodeLock.Wait();
                
                if (expand == _isExpanded) return;

                _isExpanded = expand;

                SuspendLayout();
                var cb = new CancellableEventArgs<TreeNodeEventArgs>(new TreeNodeEventArgs(this));
                if (_isExpanded)
                    OnBeforeTreeNodeExpanded(cb);
                else
                    OnBeforeTreeNodeCollapsed(cb);
                if (cb.Cancel) return;
                
                var controls = Controls.Cast<Control>().ToList();
                for (int i = 0; i < controls.Count; i++)
                    if (i > (_header == null ? -1 : 0) && i < controls.Count - (_footer == null ? 0 : 1))
                    {
                        controls[i].SuspendLayout();
                        controls[i].Visible = _isExpanded;
                        controls[i].ResumeLayout();
                    }

                if (_isExpanded)
                    OnAfterTreeNodeExpanded(new TreeNodeEventArgs(this));
                else
                    OnAfterTreeNodeCollapsed(new TreeNodeEventArgs(this));
                
                
            }
            finally
            {
                ResumeLayout();
                TreeNodeLock.Release();
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            OnTreeNodeRefresh(new TreeNodeEventArgs(this));
        }

        public void RefreshNodeLayout(bool deep=false)
        {
            SuspendLayout();
            RefreshNodeMargin();
            RefreshNodeWidth();
            if(deep)
                TreeNodes.ToList().ForEach(v=>((TreeVFlowNode)v).RefreshNodeLayout(true));
            ResumeLayout();
        }
        
        protected void RefreshNodeMargin()
        {
            var mMargin = Margin;
            mMargin.Left = (TreeLevel * LevelIndent);
            Margin = mMargin;
        }
        
        protected void RefreshNodeWidth()
        {
            if (Parent != null)
                Width = Parent.ClientSize.Width - Margin.Horizontal
                   - (PanelContainer==null?0: PanelContainer.VerticalScroll.Visible?SystemInformation.VerticalScrollBarWidth:0);

            Controls.Cast<Control>()
                .ToList().ForEach(v =>
                {
                    v.SuspendLayout();
                    v.Width = Width;
                    v.ResumeLayout();
                });
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
            {
                ret= treeNode.ContentDeepFirstOrDefault(predicate);
                if(ret!=null) return ret;
            }

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