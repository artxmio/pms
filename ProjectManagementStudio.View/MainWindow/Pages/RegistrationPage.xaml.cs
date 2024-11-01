using System.Windows.Controls;
using System.Windows.Navigation;
using ProjectManagementStudio;

namespace ProjectManagementStudio.View.MainWindow.Pages
{

    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            NavigationService.Navigate(new Uri("../ProjectManagementStudio.View;component/MainWindow/Pages/LoginPage.xaml", UriKind.Relative));

            e.Handled = true; 
        }
    }
}
