using ProjectManagementStudio.View.MainWindow.Pages;
using System.Windows.Input;
using System.Windows; 

namespace ProjectManagementStudio.View.MainWindow;

public partial class MainWindow : IMainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        RegistrationFrame.Navigate(new RegistrationPage());
    }

    private void Image_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Application.Current.Shutdown();
    }
}