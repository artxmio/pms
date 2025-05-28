using Autofac;
using ProjectManagementStudio.View.MainWindow.Pages;
using ProjectManagementStudio.View.MenuWindow.pages;
using ProjectManagementStudio.View.MenuWindow.Pages;
using ProjectManagementStudio.View.PageFactory;
using ProjectManagementStudio.ViewModel.Pages;

namespace ProjectManagementStudio.Bootstrapper.Factories;

internal class PageFactory : IPageFactory
{
    private readonly IComponentContext _componentContext;

    public PageFactory(IComponentContext componentContext)
    {
        _componentContext = componentContext;
    }

    public IPage Create(Pages pageKey)
    {
        return pageKey switch
        {
            Pages.LoginPage => _componentContext.Resolve<ILoginPage>(),
            Pages.RegistrationPage => _componentContext.Resolve<IRegistrationPage>(),
            Pages.WelcomePage => _componentContext.Resolve<IWelcomePage>(),
            Pages.ProfilePage => _componentContext.Resolve<IProfilePage>(),
            Pages.SettingsPage => _componentContext.Resolve<ISettingsPage>(),
            Pages.ProjectPage => _componentContext.Resolve<IProjectPage>(),
            Pages.ProjectDetailsPage => _componentContext.Resolve<IProjectDetailsPage>(),
            _ => throw new ArgumentException($"No such page: {pageKey}")
        };
    }
}