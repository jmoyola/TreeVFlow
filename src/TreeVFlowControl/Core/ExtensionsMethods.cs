namespace System.Windows.Forms
{
    public static class SystemWindowsFormsExtensionsMethods
    {
        public static int TotalHeight(this Control control)
        {
            return control.Visible ? control.Height + control.Margin.Vertical : 0;
        }
    }
}
namespace System.Collections.Generic
{
    public static class SystemCollectionGenericsExtensionsMethods
    {
        public static void ForEachIndex<T>(this IList<T> control, Action<int, T> action)
        {
            for(int i = 0; i < control.Count; i++)
                action(i, control[i]);
        }
    }
}