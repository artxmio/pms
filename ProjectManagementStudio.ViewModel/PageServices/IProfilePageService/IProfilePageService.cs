using ProjectManagementStudio.ViewModel.Windows;
using System.Windows.Media.Imaging;

namespace ProjectManagementStudio.ViewModel.PageServices.IProfilePageService;

public interface IProfilePageService
{
    BitmapImage AvatarImage { get; set; }
    public string AboutText { get; set; }

    BitmapImage ChangeAvatar();
    void OpenChangeLoginWindow();
    void OpenChangePasswordWindow();
    void OpenChangeEmailWindow();
    void OpenChangeAboutTextWindow();
    void Logout(IWindowViewModel viewModel);
}
