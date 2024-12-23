using Autofac;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.MainWindow;
using ProjectManagementStudio.ViewModel.MenuWindow;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.NewEmail;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.NewLogin;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.NewPassword;

namespace ProjectManagementStudio.ViewModel.RegistrationModule;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>().SingleInstance();
        builder.RegisterType<MenuWindowViewModel>().As<IMenuWindowViewModel>().SingleInstance();
        builder.RegisterType<ChangeLoginModalWindowViewModel>().As<IChangeLoginModalWindowViewModel>().SingleInstance();
        builder.RegisterType<ChangePasswordModalWindowViewModel>().As<IChangePasswordModalWindowViewModel>().SingleInstance();
        builder.RegisterType<ChangeEmailModalWindowViewModel>().As<IChangeEmailModalWindowViewModel>().SingleInstance();

        builder.RegisterType<APIClient.APIClient>().As<IAPIClient>().SingleInstance();

        builder.RegisterType<WindowMediator.WindowMediator>().As<IWindowMediator>().SingleInstance();
    }
}