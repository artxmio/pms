using Autofac;
using ProjectManagementStudio.Bootstrapper.Factories;
using ProjectManagementStudio.Bootstrapper.PathService;
using ProjectManagementStudio.Model.PathService;
using ProjectManagementStudio.View.WindowFactory;

namespace ProjectManagementStudio.Bootstrapper.RegistrationModule;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<WindowFactory>().As<IWindowFactory>().SingleInstance();

        builder.RegisterType<PathService.PathService>()
            .As<IPathService>()
            .As<IPathServiceInitializer>()
            .SingleInstance();
    }
}