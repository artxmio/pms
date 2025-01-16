using ProjectManagementStudio.ViewModel.MenuWindow;
using System.Windows;

namespace ProjectManagementStudio.View.MenuWindow;

public partial class MenuWindow : IMenuWindow
{
    public MenuWindow(IMenuWindowViewModel viewModel)
    {
        InitializeComponent();
    }
}