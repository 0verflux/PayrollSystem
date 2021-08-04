using PayrollSystem.UI.Contracts.Views;
using PayrollSystem.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PayrollSystem.UI.Views
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window, IShellWindow
    {
        public ShellWindow(ShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        #region IShellWindow Implementation
        public void CloseWindow()
        {
            Close();
        }
        public void ShowWindow()
        {
            Show();
        }
        public Frame GetNavigationFrame()
        {
            return shellFrame;
        }

        #endregion
    }
}
