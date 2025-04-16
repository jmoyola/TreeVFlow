using System;
using System.Collections.Generic;
using TreeVFlowControl.Core;

namespace TreeVFlowControl.Imp;

public class TreeNode<T>:ITreeNode<T>
{
    private readonly IList<INode<T>> _treeNodes = new List<INode<T>>();
    
    public T Value { get; set; }
    public ITreeNode<T> ParentTreeNode { get; internal set; }
    public ITreeNode<T> RootTreeNode=>ParentTreeNode is ITreeNode<T>? ParentTreeNode.RootTreeNode : this;
    public int TreeLevel => ParentTreeNode?.TreeLevel?? 0;
    public IList<ITreeNode<T>> TreeNodes => _treeNodes;
    
    public void ClearTreeNodes()
    {
        _treeNodes.Clear();
    }

    public ITreeNode<T> AddTreeNode(ITreeNode<T> treeNode)
    {
        _treeNodes.Add(treeNode);
        treeNode.ParentTreeNode = this;
        return treeNode;
    }

    public void RemoveTreeNode(ITreeNode<T> treeNodeToRemove)
    {
        _treeNodes.Remove(treeNodeToRemove);
    }

    public IList<INode<T>> TreeContentNodes { get; }
    public void ClearTreeContentNodes()
    {
        throw new NotImplementedException();
    }

    public INode<T> AddTreeContentNode()
    {
        throw new NotImplementedException();
    }

    public void RemoveTreeContentNode(INode<T> treeNodeToRemove)
    {
        throw new NotImplementedException();
    }

    public ITreeNode<T> TreeDeepFirstOrDefault(Func<ITreeNode<T>, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ITreeNode<T>> TreeDeepWhere(Func<ITreeNode<T>, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public INode<T> TreeContentDeepFirstOrDefault(Func<INode<T>, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<INode<T>> TreeContentDeepWhere(Func<INode<T>, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public void AddContent(T content)
    {
        throw new NotImplementedException();
    }

    public void RemoveContent(T content)
    {
        throw new NotImplementedException();
    }

    public void ClearContent()
    {
        throw new NotImplementedException();
    }

    public IList<T> TreeContent { get; }
    public void ClearAll()
    {
        throw new NotImplementedException();
    }
}