using Autofac;
using ProjectManagementStudio.Bootstrapper.Factories;
using ProjectManagementStudio.Bootstrapper.LocalizationService;
using ProjectManagementStudio.Bootstrapper.Services.AvatarService;
using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Bootstrapper.Services.PageServices.ProfilePageService;
using ProjectManagementStudio.Bootstrapper.Services.PageServices.SettingsPageService;
using ProjectManagementStudio.Bootstrapper.Services.PathService;
using ProjectManagementStudio.Bootstrapper.Services.Settings.SettingSizeService;
using ProjectManagementStudio.Bootstrapper.Services.UrlService;
using ProjectManagementStudio.Model.PathService;
using ProjectManagementStudio.View.PageFactory;
using ProjectManagementStudio.View.WindowFactory;
using ProjectManagementStudio.ViewModel.AvatarService;
using ProjectManagementStudio.ViewModel.PageServices.IProfilePageService;
using ProjectManagementStudio.ViewModel.PageServices.ISettingsPageService;
using ProjectManagementStudio.ViewModel.SettingSizeService;
using ProjectManagementStudio.ViewModel.UrlService;

namespace ProjectManagementStudio.Bootstrapper.RegistrationModule;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<WindowFactory>().As<IWindowFactory>().SingleInstance();
        builder.RegisterType<PageFactory>().As<IPageFactory>().SingleInstance();
        
        builder.RegisterType<LocalizationService.LocalizationService>().As<ILocalizationService>().SingleInstance();

        builder.RegisterType<PathService>()
            .As<IPathService>()
            .As<IPathServiceInitializer>()
            .SingleInstance();

        builder.RegisterType<UrlService>()
            .As<IUrlService>()
            .As<IUrlServiceInitializer>()
            .SingleInstance();

        builder.RegisterType<AvatarService>()
            .As<IAvatarService>()
            .As<IAvatarServiceInitializer>()
            .SingleInstance();

        builder.RegisterType<CurrentUserService>()
            .As<ICurrentUserService>()
            .As<ICurrentUserServiceInitializer>()
            .SingleInstance();

        builder.RegisterType<ProfilePageService>()
            .As<IProfilePageService>()
            .As<IProfilePageServiceInitializer>()
            .SingleInstance();

        builder.RegisterType<SettingsPageService>()
            .As<ISettingsPageService>()
            .As<ISettingsPageServiceInitializer>()
            .SingleInstance();

        builder.RegisterType<SettingSizeService>()
            .As<ISettingSizeService>()
            .As<ISettingSizeInitialize>()
            .SingleInstance();
    }
}