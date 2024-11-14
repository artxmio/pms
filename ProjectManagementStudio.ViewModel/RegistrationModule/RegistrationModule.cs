using Autofac;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.MainWindow;

namespace ProjectManagementStudio.ViewModel.RegistrationModule;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>().InstancePerDependency();
        builder.RegisterType<APIClient.APIClient>().As<IAPIClient>().SingleInstance();
    }
}