using ProjectManagementStudio.View.MainWindow.Pages;
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
            _ => throw new ArgumentException($"No such page: {pageKey}")
        };
    }
}