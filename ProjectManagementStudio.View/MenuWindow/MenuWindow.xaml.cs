using System.Windows;

namespace ProjectManagementStudio.View.MenuWindow;

public partial class MenuWindow : IMenuWindow
{
    public MenuWindow()
    {
        InitializeComponent();
        WelcomeFrame.Navigate(new pages.WelcomePage());
    }

    private void WelcomeFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {

    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}