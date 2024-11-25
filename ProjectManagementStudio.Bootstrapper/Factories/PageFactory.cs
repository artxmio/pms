using ProjectManagementStudio.View.MainWindow.Pages;
using ProjectManagementStudio.View.MenuWindow.Pages;
using ProjectManagementStudio.View.PageFactory;
using ProjectManagementStudio.ViewModel.Pages;

namespace ProjectManagementStudio.Bootstrapper.Factories;

internal class PageFactory : IPageFactory
{
    public IPage Create(Pages pageKey)
    {
        return pageKey switch
        {
            Pages.LoginPage => new LoginPage(),
            Pages.RegistrationPage => new RegistrationPage(),
            Pages.WelcomepPage => new WelcomePage(),
            Pages.ProfilePage => new ProfilePage(),
            _ => throw new ArgumentException($"No such page: {pageKey}")
        };
    }
}