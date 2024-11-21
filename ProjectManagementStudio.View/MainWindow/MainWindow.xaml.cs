using ProjectManagementStudio.ViewModel.MainWindow;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ProjectManagementStudio.View.MainWindow;

public partial class MainWindow : IMainWindow
{
    public MainWindow(IMainWindowViewModel viewModel)
    {
        InitializeComponent();
    }
}