using System.Windows.Controls;
using System.Windows.Navigation;

namespace ProjectManagementStudio.View.MainWindow.Pages;

public partial class RegistrationPage : Page, IRegistrationPage
{
    public RegistrationPage()
    {
        InitializeComponent();
    }

    private void ShowPassword(object sender, System.Windows.RoutedEventArgs e)
    {
        password.IsPasswordVisible = !password.IsPasswordVisible;
    }
}