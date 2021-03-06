using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace PayrollSystem.UI.Behaviors
{
    // source: https://www.codeproject.com/Tips/1249276/WPF-Select-All-Focus-Behavior
    public class SelectAllFocusBehavior
    {
        public static bool GetEnable(FrameworkElement frameworkElement)
        {
            return (bool)frameworkElement.GetValue(EnableProperty);
        }

        public static void SetEnable(FrameworkElement frameworkElement, bool value)
        {
            frameworkElement.SetValue(EnableProperty, value);
        }

        public static readonly DependencyProperty EnableProperty =
                 DependencyProperty.RegisterAttached("Enable",
                    typeof(bool), typeof(SelectAllFocusBehavior),
                    new FrameworkPropertyMetadata(false, OnEnableChanged));

        private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement frameworkElement || (e.NewValue is bool) == false)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                frameworkElement.GotFocus += SelectAll;
                frameworkElement.PreviewMouseDown += IgnoreMouseButton;
            }
            else
            {
                frameworkElement.GotFocus -= SelectAll;
                frameworkElement.PreviewMouseDown -= IgnoreMouseButton;
            }
        }

        private static void SelectAll(object sender, RoutedEventArgs e)
        {
            var frameworkElement = e.OriginalSource as FrameworkElement;

            switch (frameworkElement)
            {
                case TextBox tbox:
                    tbox.SelectAll();
                    break;
                case PasswordBox pbox:
                    pbox.SelectAll();
                    break;
                default:
                    break;
            }
        }

        private static void IgnoreMouseButton(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is not FrameworkElement frameworkElement || frameworkElement.IsKeyboardFocusWithin)
            {
                return;
            }

            e.Handled = true;
            frameworkElement.Focus();
        }
    }
}
