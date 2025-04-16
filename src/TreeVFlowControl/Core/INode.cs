using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TreeVFlowControl.Core
{
    public interface INode<T>
    {
        T Value { get; set; }
    }
}