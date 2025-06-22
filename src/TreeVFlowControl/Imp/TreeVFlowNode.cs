using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private bool _isEnabled=true;
        private bool _isVisible=true;
        
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
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeExpandedChanged;
        public event TreeNodeEventHandler AfterTreeNodeExpandedChanged;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeAdded;
        public event TreeNodeEventHandler AfterTreeNodeAdded;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeRemoved;
        public event TreeNodeEventHandler AfterTreeNodeRemoved;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeContentNodeAdded;
        public event TreeNodeEventHandler AfterContentNodeAdded;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeContentNodeRemoved;
        public event TreeNodeEventHandler AfterContentNodeRemoved;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeEnabledChanged;
        public event TreeNodeEventHandler AfterTreeNodeEnabledChanged;
        public event CancellableEventHandler<TreeNodeEventArgs> BeforeTreeNodeVisibleChanged;
        public event TreeNodeEventHandler AfterTreeNodeVisibleChanged;

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
        protected virtual void OnBeforeTreeNodeExpandedChanged(CancellableEventArgs<TreeNodeEventArgs> args)
        {
            BeforeTreeNodeExpandedChanged?.Invoke(this, args);
        }
        protected virtual void OnAfterTreeNodeExpandedChanged(TreeNodeEventArgs args)
        {
            AfterTreeNodeExpandedChanged?.Invoke(this, args);
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
        protected virtual void OnBeforeTreeNodeEnabledChanged(CancellableEventArgs<TreeNodeEventArgs> args)
        {
            BeforeTreeNodeEnabledChanged?.Invoke(this, args);
        }
        protected virtual void OnAfterTreeNodeEnabledChanged(TreeNodeEventArgs args)
        {
            AfterTreeNodeEnabledChanged?.Invoke(this, args);
        }
        protected virtual void OnBeforeTreeNodeVisibleChanged(CancellableEventArgs<TreeNodeEventArgs> args)
        {
            BeforeTreeNodeVisibleChanged?.Invoke(this, args);
        }
        protected virtual void OnAfterTreeNodeVisibleChanged(TreeNodeEventArgs args)
        {
            AfterTreeNodeVisibleChanged?.Invoke(this, args);
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
        
        protected void OnAddContent(Control content, Action<Control> addAction)
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
                addAction?.Invoke(content);

                
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
        
        public virtual void AddContent(Control content)
        {
            OnAddContent(content, (c)=>{
                Controls.Add(c);
                Controls.SetChildIndex(c, Controls.Count - (_footer == null ? 1 : 2));
            });
        }

        public virtual void InsertContent(Control content, int index)
        {
            OnAddContent(content, (c)=>{
                Controls.Add(c);
                Controls.SetChildIndex(c, (_header == null ? 0 : 1) + index);
            });
        }
        
        public virtual void InsertContent(Control content, Func<object, object, int> comparator)
        {
            OnAddContent(content, (c)=>{
                Controls.Add(c);
                
                int offset = (_header == null ? 0 : 1);
                int end = TreeContent.Count + offset - 1;
                int index;
                for (index = offset; index < end; index++)
                {
                    if (comparator(content, Controls[index]) > 0)
                        break;
                }
                Controls.SetChildIndex(c, index);
            });
        }
        
        public virtual void RemoveContent(Control content)
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
        
        private IGraphicalTreeNode OnAddTreeNode(IGraphicalTreeNode newTreeNode, Action<TreeVFlowNode> addAction)
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
                
                addAction(newTreeVFlowNode);
                
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

        public virtual IGraphicalTreeNode AddTreeNode(IGraphicalTreeNode newTreeNode)
        {
            return OnAddTreeNode(newTreeNode, node =>{
                Controls.Add(node);
                Controls.SetChildIndex(node, Controls.Count - (_footer == null ? 1 : 2));
            } );
        }
        
        public virtual IGraphicalTreeNode InsertTreeNode(IGraphicalTreeNode newTreeNode, int index)
        {
            return OnAddTreeNode(newTreeNode, node =>{
                Controls.Add(node);
                Controls.SetChildIndex(node, (_header == null ? 0 : 1)+index);
            });
        }

        public virtual IGraphicalTreeNode InsertTreeNode(IGraphicalTreeNode newTreeNode,
            Func<object, object, int> comparator)
        {
            return OnAddTreeNode(newTreeNode, node =>{
                Controls.Add(node);
                
                int offset = (_header == null ? 0 : 1);
                int end = TreeNodes.Count + offset;
                int index;
                for (index = offset; index < end; index++)
                {
                    if (comparator(newTreeNode, Controls[index]) > 0)
                        break;
                }
                Controls.SetChildIndex(node, index);
            });
        }
        
        public virtual void RemoveTreeNode(IGraphicalTreeNode treeNodeToRemove)
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
        
        public virtual bool IsEnabled=>_isEnabled;
        
        public void SetEnabled(bool enabled)
        {
            if (_isEnabled==enabled) return;

            var bc = new CancellableEventArgs<TreeNodeEventArgs>(new TreeNodeEventArgs(this));
            OnBeforeTreeNodeEnabledChanged(bc);
            if (bc.Cancel) return;
            
            OnSetEnabled(enabled);
            _isEnabled = enabled;
            
            OnAfterTreeNodeEnabledChanged(new TreeNodeEventArgs(this));
        }
        
        protected virtual void OnSetEnabled(bool enabled)
        {
            Controls.Cast<Control>().ToList().ForEach(v=>v.Enabled=enabled);
        }
        
        public void SetVisible(bool visible)
        {
            if (_isVisible==visible) return;

            var bc = new CancellableEventArgs<TreeNodeEventArgs>(new TreeNodeEventArgs(this));
            OnBeforeTreeNodeVisibleChanged(bc);
            if (bc.Cancel) return;
            
            OnSetVisible(visible);
            _isVisible = visible;
            
            OnAfterTreeNodeVisibleChanged(new TreeNodeEventArgs(this));
        }
        
        protected virtual void OnSetVisible(bool visible)
        {
            Controls.Cast<Control>().ToList().ForEach(v=>v.Visible=visible);
        }
        
        public virtual bool IsVisible=>_isVisible;
        
        public bool IsExpanded=>_isExpanded;
        public void ToggleItems()
        {
            SetExpanded(!_isExpanded);
        }
        
        public void SetExpanded(bool expanded) {
            try
            {
                TreeNodeLock.Wait();
                
                if (expanded == _isExpanded) return;

                SuspendLayout();
                var cb = new CancellableEventArgs<TreeNodeEventArgs>(new TreeNodeEventArgs(this));
 
                OnBeforeTreeNodeExpandedChanged(cb);

                if (cb.Cancel) return;
                
                OnSetExpanded(expanded);
                _isExpanded = expanded;
                
                OnAfterTreeNodeExpandedChanged(new TreeNodeEventArgs(this));
            }
            finally
            {
                ResumeLayout();
                TreeNodeLock.Release();
            }
        }

        protected virtual void OnSetExpanded(bool expanded)
        {
            var controls = Controls.Cast<Control>().ToList();
            for (int i = 0; i < controls.Count; i++)
            {
                if (i > (_header == null ? -1 : 0) && i < controls.Count - (_footer == null ? 0 : 1))
                {
                    controls[i].SuspendLayout();
                    controls[i].Visible = expanded;
                    controls[i].ResumeLayout();
                }
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
        
        protected void UpdateUi(Action updateAction)
        {
            if (IsDisposed || !IsHandleCreated)
                return;

            if (InvokeRequired)
                Invoke(updateAction);
            else
                updateAction();
        }
        
        protected async Task UpdateUiAsync(Action updateAction)
        {
            if (IsDisposed || !IsHandleCreated)
                await Task.CompletedTask;

            if (InvokeRequired)
                 await Task.Run(() => Invoke(updateAction));
            else
                await Task.Run(() => updateAction);
        }
    }
}