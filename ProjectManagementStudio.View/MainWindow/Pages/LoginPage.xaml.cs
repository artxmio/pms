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

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        // Открытие нового окна MenuWindow
        MenuWindow.MenuWindow menuWindow = new MenuWindow.MenuWindow();
        menuWindow.Show();

        Window parentWindow = Window.GetWindow(this); // Получение окна, содержащего текущую страницу
        if (parentWindow != null)
        {
            parentWindow.Close();
        }
    }
}