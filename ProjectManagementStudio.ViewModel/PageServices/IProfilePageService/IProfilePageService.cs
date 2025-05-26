using ProjectManagementStudio.ViewModel.Windows;
using System.Windows.Media.Imaging;

namespace ProjectManagementStudio.ViewModel.PageServices.IProfilePageService;

public interface IProfilePageService
{
    public string AboutText { get; set; }

    void OpenChangeLoginWindow();
    void OpenChangePasswordWindow();
    void OpenChangeEmailWindow();
    void OpenChangeAboutTextWindow();
    void Logout(IWindowViewModel viewModel);
}
