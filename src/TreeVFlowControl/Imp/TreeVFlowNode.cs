using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TreeVFlowControl.Core;

namespace TreeVFlowControl.Imp
{
    public class TreeVFlowNode: TableLayoutPanel, IGraphicalTreeNode
    {
        private readonly SemaphoreSlim _nodeLock = new (1, 1);
        
        private int _suspendLayoutCount;
        
        private int _lastHeight;
        private int _lastWidth;
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
                    TreeNodeLock.Release();
                }
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
                try
                {
                    TreeNodeLock.Wait();
                    
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
                    TreeNodeLock.Release();
                }
            }
        }

        public IGraphicalTreeNode ParentTreeNode => Parent as TreeVFlowNode;
        public IGraphicalTreeNode RootTreeNode=>ParentTreeNode==null?this:ParentTreeNode.RootTreeNode;
        public int TreeLevel => ParentTreeNode == null ? 0 : ParentTreeNode.TreeLevel + 1;
        
        private void ContentClick(object sender, EventArgs args)
        {
            if (sender == _header)
                OnTreeNodeHeaderClick(new TreeNodeEventArgs(this, (Control)sender));
            else if (sender == _footer)
                OnTreeNodeHeaderClick(new TreeNodeEventArgs(this, (Control)sender));
            else
                OnContentNodeClick(new TreeNodeEventArgs(this, (Control)sender));

        }
        private void ContentDoubleClick(object sender, EventArgs args)
        {
            if (sender == _header)
                OnTreeNodeHeaderDoubleClick(new TreeNodeEventArgs(this, (Control)sender));
            else if (sender == _footer)
                OnTreeNodeHeaderDoubleClick(new TreeNodeEventArgs(this, (Control)sender));
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
        public event TreeNodeEventHandler TreeNodeCollapsed;
        public event TreeNodeEventHandler TreeNodeExpanded;
        public event TreeNodeEventHandler TreeNodeAdded;
        public event TreeNodeEventHandler TreeNodeRemoved;
        public event TreeNodeEventHandler ContentNodeAdded;
        public event TreeNodeEventHandler ContentNodeRemoved;
        public event TreeNodeEventHandler TreeNodeRefresh;
        public event TreeNodeEventHandler ResizeHeight;
        public event TreeNodeEventHandler ResizeWidth;
        
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
        protected virtual void OnTreeNodeExpanded(TreeNodeEventArgs args)
        {
            TreeNodeExpanded?.Invoke(this, args);
        }
        protected virtual void OnTreeNodeCollapsed(TreeNodeEventArgs args)
        {
            TreeNodeCollapsed?.Invoke(this, args);
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
        protected virtual void OnResizeWidth(TreeNodeEventArgs args)
        {
            ResizeWidth?.Invoke(this, args);
        }
        protected virtual void OnResizeHeight(TreeNodeEventArgs args)
        {
            ResizeHeight?.Invoke(this, args);
        }

#endregion

        private void SubControlAdded(Control control)
        {
            control.Left = LevelIndent * TreeLevel;
            control.Width = Width;
            control.Click += ContentClick;
            control.DoubleClick += ContentDoubleClick;
            //control.SizeChanged += ControlResize;
            control.VisibleChanged += ControlResize;
        }
        
        private void SubControlRemoved(Control control)
        {
            control.Click -= ContentClick;
            control.DoubleClick -= ContentDoubleClick;
            //control.SizeChanged -= ControlResize;
            control.VisibleChanged -= ControlResize;
        }
        
        public void AddContent(Control content)
        {
            try
            {
                TreeNodeLock.Wait();
                
                if (content == null) throw new ArgumentNullException(nameof(content));
                if (TreeNodes.Count > 0) return;

                Controls.Add(content);
                Controls.SetChildIndex(content, Controls.Count - (_footer == null ? 1 : 2));
                SubControlAdded(content);
                
                OnContentNodeAdded(new TreeNodeEventArgs(this, content));
            }
            finally
            {
                TreeNodeLock.Release();
            }
        }

        public void RemoveContent(Control content)
        {
            try
            {
                TreeNodeLock.Wait();
                
                Controls.Remove(content);
                SubControlRemoved(content);

                OnContentNodeRemoved(new TreeNodeEventArgs(this, content));
            }
            finally
            {
                TreeNodeLock.Release();
            }
        }
        
        public void ClearContent()
        {
            try
            {
                TreeNodeLock.Wait();
                
                TreeContent.ToList().ForEach(RemoveContent);
            }
            finally
            {
                TreeNodeLock.Release();
            }
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
            try
            {
                TreeNodeLock.Wait();
                
                var newTreeVFlowNode = newTreeNode as TreeVFlowNode;
                if (newTreeVFlowNode == null) throw new ArgumentNullException(nameof(newTreeNode));

                SuspendLayout();
                newTreeVFlowNode.SuspendLayout();
                newTreeVFlowNode.Visible = true;
                newTreeVFlowNode.LevelIndent = LevelIndent;
                newTreeVFlowNode.ColumnCount = ColumnCount;
                newTreeVFlowNode.AutoSize = false;
                Controls.Add(newTreeVFlowNode);
                Controls.SetChildIndex(newTreeVFlowNode, Controls.Count - (_footer == null ? 1 : 2));
                
                RefreshWidth(newTreeVFlowNode);
                RefreshMargin(newTreeVFlowNode);
                newTreeVFlowNode.RefreshHeight();
                //newTreeVFlowNode.Width = ((TreeVFlowNode)ParentTreeNode)?.Width??0 -newTreeVFlowNode.Margin.Horizontal ;

                OnTreeNodeAdded(new TreeNodeEventArgs(newTreeVFlowNode));
                
                
                newTreeVFlowNode.ResumeLayout();
                ResumeLayout();
                return newTreeVFlowNode;
            }
            finally
            {
                TreeNodeLock.Release();
            }
        }
        
        public void RemoveTreeNode(IGraphicalTreeNode treeNodeToRemove)
        {
            try
            {
                TreeNodeLock.Wait();
                
                var treeNodeToRemoveToRemove = treeNodeToRemove as TreeVFlowNode;
                if (treeNodeToRemoveToRemove == null) throw new ArgumentNullException(nameof(treeNodeToRemove));

                SuspendLayoutNode();
                Controls.Remove(treeNodeToRemoveToRemove);
                RefreshHeight();
                ResumeLayoutNode();

                OnTreeNodeRemoved(new TreeNodeEventArgs(treeNodeToRemoveToRemove));
            }
            finally
            {
                TreeNodeLock.Release();
            }
        }
        
        public void RemoveTreeNode()
        {
            ParentTreeNode?.RemoveTreeNode(this);
        }
        
        public void ClearTreeNodes()
        {
            try
            {
                TreeNodeLock.Wait();
                TreeNodes.ToList().ForEach(RemoveTreeNode);
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
                ClearContent();
            }
            finally
            {
                TreeNodeLock.Release();
            }
        }
        
        public virtual bool IsDisabled=>_isDisabled;
        public virtual void DisableItem()
        {
            if (_isDisabled) return;
            
            Controls.Cast<Control>().ToList().ForEach(v=>v.Enabled=false);
            _isDisabled = true;
        }
    
        public virtual void EnableItem()
        {
            if (!_isDisabled) return;
        
            Controls.Cast<Control>().ToList().ForEach(v=>v.Enabled=true);
            _isDisabled = false;
        }
        
        public bool IsExpanded=>_isExpanded;
        public void ToggleItems()
        {
            if(_isExpanded)
                Expand(false);
            else
                Expand(true);
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

                //RootTreeNode.SuspendLayoutDeep();
                var controls = Controls.Cast<Control>().ToList();
                for (int i = 0; i < controls.Count; i++)
                    if (i > (_header == null ? -1 : 0) && i < controls.Count - (_footer == null ? 0 : 1))
                        controls[i].Visible = _isExpanded;

                RefreshHeight();

                //RootTreeNode.ResumeLayoutDeep();

                if (_isExpanded)
                    OnTreeNodeExpanded(new TreeNodeEventArgs(this));
                else
                    OnTreeNodeCollapsed(new TreeNodeEventArgs(this));
            }
            finally
            {
                TreeNodeLock.Release();
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            OnTreeNodeRefresh(new TreeNodeEventArgs(this));
        }
        
        private int ControlHeight(Control control)
        {
            if(control==null) return 0;
            return control.Visible?control.Height + control.Margin.Vertical:0;
        }

        protected override void OnResize(EventArgs eventargs)
        {
            //if (_lastWidth != Width)
            //{
            //    OnResizeWidth(_lastWidth, Width);
            //    _lastWidth = Width;
            //}
            
            //if (_lastHeight != Height)
            //{
            //    OnResizeHeight(_lastHeight, Height);
            //    _lastHeight = Height;
            //}

            base.OnResize(eventargs);
        }
        
        private void OnResizeHeight(int lastHeight, int newHeight)
        {
            OnResizeHeight(new TreeNodeEventArgs(this));
        }
        
        public virtual void OnResizeWidth(int lastWidth, int newWidth)
        {
            if (this != RootTreeNode)
                return;
            
            SuspendLayoutDeep();
            
            RefreshWithNode(this);
            //Controls.Cast<Control>()
            //    .ToList().ForEach(v => v.Width = Width - Margin.Horizontal - 6);
            ResumeLayoutDeep();
            
            OnResizeWidth(new TreeNodeEventArgs(this));
        }
        private void RefreshWithNode(TreeVFlowNode node)
        {
            RefreshMargin(node);
            RefreshWidth(node);
            TreeNodes.ToList().ForEach(v=>RefreshWithNode(v as TreeVFlowNode));
        }

        public void SuspendLayoutDeep()
        {
            if (_suspendLayoutCount == 0)
            {
                Debug.WriteLine($"SuspendLayoutDeep ({this}) do it!");
                SuspendLayout();
                Controls.Cast<Control>().ToList().ForEach(v =>
                {
                    if(v is TreeVFlowNode tvNode)
                        tvNode.SuspendLayoutDeep();
                    else
                        v.SuspendLayout();
                });
            }

            _suspendLayoutCount++;
            
            SuspendLayoutNode();
            TreeNodes.ToList().ForEach(v=>v.SuspendLayoutNode());
        }
        
        public void SuspendLayoutNode()
        {
            if (_suspendLayoutCount == 0)
            {
                Debug.WriteLine($"SuspendLayoutNode ({this}) do it!");
                SuspendLayout();
                Controls.Cast<Control>().ToList().ForEach(v => v.SuspendLayout());
            }

            _suspendLayoutCount++;
        }
        
        public void ResumeLayoutDeep()
        {
            ResumeLayoutNode();
            TreeNodes.ToList().ForEach(v=>v.ResumeLayoutNode());
        }
        public void ResumeLayoutNode()
        {
            if (_suspendLayoutCount > 0)
            {
                _suspendLayoutCount--;

                if (_suspendLayoutCount == 0)
                {
                    Debug.WriteLine($"ResumeLayoutNode ({this}) do it!!");
                    ResumeLayout();
                    Controls.Cast<Control>().ToList().ForEach(v => v.ResumeLayout());
                }
            }
        }
        
        private void RefreshHeight()
        {
            SuspendLayoutNode();
            Height =  (!_isExpanded?ControlHeight(_header):
                Controls.Cast<Control>()
                    .ToList()
                    .Sum(v => ControlHeight(v)));
            
            ((TreeVFlowNode)ParentTreeNode)?.RefreshHeight();
            
            ResumeLayoutNode();
        }
        
        private void RefreshMargin(TreeVFlowNode node)
        {
            var mMargin = node.Margin;
            mMargin.Left = (node.TreeLevel * node.LevelIndent);
            node.Margin = mMargin;
        }
        
        private void RefreshWidth(TreeVFlowNode node)
        {
            if (node.ParentTreeNode != null)
            {
                node.Width = ((TreeVFlowNode)node.ParentTreeNode).Width - node.Margin.Horizontal - 9;
            }

            node.Controls.Cast<Control>()
                .ToList().ForEach(v => v.Width = node.Width - v.Margin.Horizontal);// (node.LevelIndent*node.TreeLevel));
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