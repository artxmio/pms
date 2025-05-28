using Autofac;
using ProjectManagementStudio.View.MainWindow;
using ProjectManagementStudio.View.MainWindow.Pages;
using ProjectManagementStudio.View.MenuWindow;
using ProjectManagementStudio.View.MenuWindow.ModalWindows;
using ProjectManagementStudio.View.MenuWindow.pages;
using ProjectManagementStudio.View.MenuWindow.Pages;
using ProjectManagementStudio.ViewModel.Pages;
using ProjectManagementStudio.ViewModel.Windows;

namespace ProjectManagementStudio.View.RegistrationModule;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MainWindow.MainWindow>().As<IMainWindow>().InstancePerDependency();
        builder.RegisterType<MenuWindow.MenuWindow>().As<IMenuWindow>().InstancePerDependency();
        builder.RegisterType<AddProjectWindow>().As<IAddProjectWindow>().InstancePerDependency();
        
        builder.RegisterType<RegistrationPage>().As<IRegistrationPage>().InstancePerDependency();
        builder.RegisterType<LoginPage>().As<ILoginPage>().InstancePerDependency();
        builder.RegisterType<WelcomePage>().As<IWelcomePage>().InstancePerDependency();
        builder.RegisterType<ProfilePage>().As<IProfilePage>().InstancePerDependency();
        builder.RegisterType<SettingsPage>().As<ISettingsPage>().InstancePerDependency();
        builder.RegisterType<ProjectPage>().As<IProjectPage>().InstancePerDependency();
        builder.RegisterType<ProjectDetailsPage>().As<IProjectDetailsPage>().InstancePerDependency();

        builder.RegisterType<LoginChangeDialog>().As<ILoginChangeDialog>().InstancePerDependency();
        builder.RegisterType<PasswordChangeDialog>().As<IPasswordChangeDialog>().InstancePerDependency();
        builder.RegisterType<EmailChangeDialog>().As<IEmailChangeDialog>().InstancePerDependency();
        builder.RegisterType<AboutTextChangeDialog>().As<IAboutTextChangeDialog>().InstancePerDependency();

        builder.RegisterType<WindowManager.WindowsManager>().As<IWindowManager>().SingleInstance();
        builder.RegisterType<PageManager.PageManager>().As<IPageManager>().SingleInstance();
    }
}