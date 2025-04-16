using System;
using TreeVFlowControl.Core;

namespace TreeVFlowControl.Imp;

public class Node:INode<Object>
{
    public object Value { get; set; }
}