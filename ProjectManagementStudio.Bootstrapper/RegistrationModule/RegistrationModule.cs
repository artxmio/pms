using Autofac;
using ProjectManagementStudio.Bootstrapper.Factories;
using ProjectManagementStudio.Bootstrapper.Services.PathService;
using ProjectManagementStudio.Bootstrapper.Services.UrlService;
using ProjectManagementStudio.Model.PathService;
using ProjectManagementStudio.View.WindowFactory;
using ProjectManagementStudio.ViewModel.UrlService;

namespace ProjectManagementStudio.Bootstrapper.RegistrationModule;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<WindowFactory>().As<IWindowFactory>().SingleInstance();

        builder.RegisterType<PathService>()
            .As<IPathService>()
            .As<IPathServiceInitializer>()
            .SingleInstance();

        builder.RegisterType<UrlService>()
            .As<IUrlService>()
            .As<IUrlServiceInitializer>()
            .SingleInstance();
    }
}