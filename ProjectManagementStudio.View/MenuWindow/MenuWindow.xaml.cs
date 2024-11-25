using ProjectManagementStudio.ViewModel.MenuWindow;

namespace ProjectManagementStudio.View.MenuWindow;

public partial class MenuWindow : IMenuWindow
{
    public MenuWindow(IMenuWindowViewModel viewModel)
    {
        InitializeComponent();
    }
}