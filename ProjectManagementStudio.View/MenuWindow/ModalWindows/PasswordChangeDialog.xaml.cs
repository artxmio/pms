namespace ProjectManagementStudio.View.MenuWindow.ModalWindows;

public partial class PasswordChangeDialog : IPasswordChangeDialog
{
    public PasswordChangeDialog()
    {
        InitializeComponent();
    }

    private void ShowOldPassword(object sender, System.Windows.RoutedEventArgs e)
    {
        oldPassword.IsPasswordVisible = !oldPassword.IsPasswordVisible;
    }

    private void ShowNewPassword(object sender, System.Windows.RoutedEventArgs e)
    {
        newPassword.IsPasswordVisible = !newPassword.IsPasswordVisible;
    }
}
