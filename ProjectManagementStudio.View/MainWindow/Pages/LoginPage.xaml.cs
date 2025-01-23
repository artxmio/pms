using ProjectManagementStudio.View.UserControls.TextPasswordBox;
using System.Windows.Controls;

namespace ProjectManagementStudio.View.MainWindow.Pages;

public partial class LoginPage : Page, ILoginPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void ShowPassword(object sender, System.Windows.RoutedEventArgs e)
    {
        password.IsPasswordVisible = !password.IsPasswordVisible;
    }
}