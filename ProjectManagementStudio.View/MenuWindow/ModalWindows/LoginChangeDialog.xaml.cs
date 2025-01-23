namespace ProjectManagementStudio.View.MenuWindow.ModalWindows;

public partial class LoginChangeDialog : ILoginChangeDialog
{
    public LoginChangeDialog()
    {
        InitializeComponent();
    }

    private void ShowPassword(object sender, System.Windows.RoutedEventArgs e)
    {
        password.IsPasswordVisible = !password.IsPasswordVisible;
    }
}