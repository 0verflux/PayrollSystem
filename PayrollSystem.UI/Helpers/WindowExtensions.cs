using System.Windows;
using System.Windows.Media;

namespace PayrollSystem.UI.Helpers
{
    public static class WindowExtensions
    {
        public static T GetParent<T>(this DependencyObject child) where T : DependencyObject
        {
            var dependencyObject = VisualTreeHelper.GetParent(child);
            return dependencyObject != null
                ? dependencyObject is T parent
                ? parent
                : GetParent<T>(dependencyObject)
                : null;
        }
    }
}
