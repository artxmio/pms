using ProjectManagementStudio.ViewModel.MenuWindow;
using System.Windows;
using System.Windows.Input;

namespace ProjectManagementStudio.View.MenuWindow;

public partial class MenuWindow : IMenuWindow
{
    public MenuWindow(IMenuWindowViewModel viewModel)
    {
        InitializeComponent();
    }

    private void DragWindow(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed) { this.DragMove(); }
    }
}