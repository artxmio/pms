using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ProjectManagementStudio.View.MainWindow.Pages;

public partial class LoginPage : Page
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        NavigationService.Navigate(new Uri("../ProjectManagementStudio.View;component/MainWindow/Pages/RegistrationPage.xaml", UriKind.Relative));

        e.Handled = true;
    }
}