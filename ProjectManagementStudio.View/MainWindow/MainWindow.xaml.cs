using ProjectManagementStudio.ViewModel.MainWindow;

namespace ProjectManagementStudio.View.MainWindow;

public partial class MainWindow : IMainWindow
{
    public MainWindow(IMainWindowViewModel viewModel)
    {
        InitializeComponent();
    }
}