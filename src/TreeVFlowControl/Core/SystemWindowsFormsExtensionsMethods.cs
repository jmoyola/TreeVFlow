namespace System.Windows.Forms;

public static class SystemWindowsFormsExtensionsMethods
{
    public static int TotalHeight(this Control control)
    {
        return control.Visible?control.Height + control.Margin.Vertical:0;
    }
}