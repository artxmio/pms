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
        
        RegistrationFrame.Navigate(new LoginPage());
    }

    private void Image_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Application.Current.Shutdown();
    }
}