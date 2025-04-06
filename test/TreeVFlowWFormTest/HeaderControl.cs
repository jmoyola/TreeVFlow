using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace HierarchicalTree
{
    public class HeaderControl:FlowLayoutPanel
    {
        public HeaderControl()
        :base()
        {
            Image image = new Bitmap(16,16);
            Label label = new Label();
            FlowDirection = FlowDirection.LeftToRight;
            //Controls.Add(image);
            Controls.Add(label);
        }
        
    }
}