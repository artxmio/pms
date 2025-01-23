namespace ProjectManagementStudio.View.MenuWindow.ModalWindows;

public partial class EmailChangeDialog : IEmailChangeDialog
{
    public EmailChangeDialog()
    {
        InitializeComponent();
    }

    private void ShowPassword(object sender, System.Windows.RoutedEventArgs e)
    {
        password.IsPasswordVisible = !password.IsPasswordVisible;
    }
}