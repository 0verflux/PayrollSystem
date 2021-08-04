using System.Windows.Controls;

namespace PayrollSystem.UI.Contracts.Views
{
    public interface IShellWindow
    {
        Frame GetNavigationFrame();
        void ShowWindow();
        void CloseWindow();
    }
}
