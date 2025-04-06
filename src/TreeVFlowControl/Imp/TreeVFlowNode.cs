﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TreeVFlowControl.Core;

namespace TreeVFlowControl.Imp
{
   
    public class TreeVFlowNode: TableLayoutPanel, IGraphicalTreeNode
    {
        public int LevelIndent { get; set; } = 10;
        private Control _header;
        private Control _footer;
        private bool _isFold;
        
        public event TreeNodeEventHandler TreeNodeSelected;
        public event TreeNodeEventHandler ContentNodeSelected;
        public event TreeNodeEventHandler TreeNodeFold;
        public event TreeNodeEventHandler TreeNodeUnfold;
        public event TreeNodeEventHandler TreeNodeAdded;
        public event TreeNodeEventHandler TreeNodeRemoved;
        public event TreeNodeEventHandler ContentNodeAdded;
        public event TreeNodeEventHandler ContentNodeRemoved;
        public event TreeNodeEventHandler TreeNodeRefresh;
        
        public TreeVFlowNode()
        : this(null) { }
        
        public TreeVFlowNode(Control header, Control footer=null)
        {
            ColumnCount = 1;
            //FlowDirection = FlowDirection.TopDown;
            
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
                }

                if (value != null)
                {
                    Controls.Add(value);
                    Controls.SetChildIndex(value, 0);
                    
                    value.DoubleClick += HeaderDoubleClick;
                    value.Click += HeaderClick;
                    value.SizeChanged += ControlResize;
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
                    _footer.SizeChanged -= ControlResize;
                }

                if (value != null)
                {
                    value.Left = LevelIndent*TreeLevel;
                    value.Width = Width;
                    Controls.Add(value);
                    Controls.SetChildIndex(value, Controls.Count-1);
                    value.SizeChanged += ControlResize;
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
            OnTreeNodeSelected(new TreeNodeEventArgs(this, null));
        }
        private void HeaderDoubleClick(object sender, EventArgs args)
        {
            TogleFold();
        }

        private void ContentClick(object sender, EventArgs args)
        {
            OnContentNodeSelected(new TreeNodeEventArgs(this, (Control)sender));
        }

#region Events

        
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
        protected virtual void OnTreeNodeSelected(TreeNodeEventArgs args)
        {
            TreeNodeSelected?.Invoke(this, args);
        }
        
        protected virtual void OnTreeNodeRefresh(TreeNodeEventArgs args)
        {
            TreeNodeRefresh?.Invoke(this, args);
        }
        protected virtual void OnContentNodeSelected(TreeNodeEventArgs args)
        {
            ContentNodeSelected?.Invoke(this, args);
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
            RefreshHeight();
            content.SizeChanged += ControlResize;
            OnContentNodeAdded(new TreeNodeEventArgs(this, content));
        }

        public void RemoveContent(Control content)
        {
            Controls.Remove(content);
            content.Click -= ContentClick;
            RefreshHeight();
            content.SizeChanged -= ControlResize;
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
            
            newTreeVFlowNode.Visible = true;
            newTreeVFlowNode.LevelIndent = LevelIndent;
            //newTreeNode.FlowDirection = FlowDirection;
            //newTreeNode.WrapContents = WrapContents;
            newTreeVFlowNode.ColumnCount = ColumnCount;
            newTreeVFlowNode.AutoSize = false;
            newTreeVFlowNode.Width = Width;
            Controls.Add(newTreeVFlowNode);
            Controls.SetChildIndex(newTreeVFlowNode, Controls.Count - (_footer==null?1:2));

            OnTreeNodeAdded(new TreeNodeEventArgs(newTreeVFlowNode, null));
            newTreeVFlowNode.RefreshHeight();

            
            return newTreeVFlowNode;
        }
        
        public void RemoveTreeNode(IGraphicalTreeNode treeNodeToRemove)
        {
            var treeNodeToRemoveToRemove = treeNodeToRemove as TreeVFlowNode; 
            if(treeNodeToRemoveToRemove==null) throw new ArgumentNullException(nameof(treeNodeToRemove));
            
            Controls.Remove(treeNodeToRemoveToRemove);
            RefreshHeight();
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

            var controls = Controls.Cast<Control>().ToList();
            for (int i = 0; i < controls.Count; i++)
                if (i > (_header==null?-1:0) && i < controls.Count - (_footer==null?0:1)) controls[i].Visible = !fold;
            
            RefreshHeight();

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

        protected override void OnClientSizeChanged(EventArgs e)
        {
            var mMargin = Margin;
            mMargin.Left=(TreeLevel * LevelIndent) + DefaultMargin.Left;
            Margin = mMargin;
            Controls.Cast<Control>()
                .ToList().ForEach(v=>v.Width = Width-Margin.Horizontal - 6);

            base.OnClientSizeChanged(e);
        }

        private void RefreshHeight()
        {
            Height = (_isFold?ControlHeight(_header):
                Controls.Cast<Control>()
                    .ToList()
                    .Sum(v => ControlHeight(v) + v.Margin.Vertical));
            
            ((TreeVFlowNode)ParentTreeNode)?.RefreshHeight();
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