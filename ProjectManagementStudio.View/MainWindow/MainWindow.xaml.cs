using ProjectManagementStudio.View.MainWindow.Pages;
using System.Windows.Input;
using System.Windows;
using ProjectManagementStudio.ViewModel.MainWindow;

namespace ProjectManagementStudio.View.MainWindow;

public partial class MainWindow : IMainWindow
{
    public MainWindow(IMainWindowViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
        
        RegistrationFrame.Navigate(new RegistrationPage());
    }
}