using Autofac;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.MainWindow;
using ProjectManagementStudio.ViewModel.MenuWindow;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.ChangeLogin;

namespace ProjectManagementStudio.ViewModel.RegistrationModule;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>().InstancePerDependency();
        builder.RegisterType<MenuWindowViewModel>().As<IMenuWindowViewModel>().InstancePerDependency();
        builder.RegisterType<LoginChangeDialogViewModel>().As<ILoginChangeDialogViewModel>().InstancePerDependency();

        builder.RegisterType<APIClient.APIClient>().As<IAPIClient>().SingleInstance();
    }
}