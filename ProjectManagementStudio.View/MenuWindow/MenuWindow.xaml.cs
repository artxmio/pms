using ProjectManagementStudio.ViewModel.MenuWindow;

namespace ProjectManagementStudio.View.MenuWindow;

public partial class MenuWindow : IMenuWindow
{
    private readonly IMenuWindowViewModel _viewModel;

    public MenuWindow(IMenuWindowViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
    }
}