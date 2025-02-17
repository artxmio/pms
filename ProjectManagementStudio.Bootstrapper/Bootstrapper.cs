using Autofac;
using ProjectManagementStudio.Bootstrapper.Services.AvatarService;
using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Bootstrapper.Services.PageServices.ProfilePageService;
using ProjectManagementStudio.Bootstrapper.Services.PageServices.SettingsPageService;
using ProjectManagementStudio.Bootstrapper.Services.PathService;
using ProjectManagementStudio.Bootstrapper.Services.Settings.SettingSizeService;
using ProjectManagementStudio.Bootstrapper.Services.UrlService;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.Model.WindowSavedData.Wrapper;
using ProjectManagementStudio.ViewModel.MainWindow;
using ProjectManagementStudio.ViewModel.MenuWindow;
using ProjectManagementStudio.ViewModel.PreloadWindow;
using ProjectManagementStudio.ViewModel.Windows;
using System.Windows;

namespace ProjectManagementStudio.Bootstrapper;

public class Bootstrapper : IDisposable
{
    private readonly IContainer _container;
    private readonly IWindowManager _windowManager;

    public Bootstrapper()
    {
        var container = new ContainerBuilder();

        container
            .RegisterModule<RegistrationModule.RegistrationModule>()
            .RegisterModule<Model.RegistrationModule.RegistrationModule>()
            .RegisterModule<ViewModel.RegistrationModule.RegistrationModule>()
            .RegisterModule<View.RegistrationModule.RegistrationModule>();

        _container = container.Build();

        _windowManager = _container.Resolve<IWindowManager>();
    }

    public Window Run()
    {
        InitializeDependencies();

        var viewModel = _container.Resolve<IPreloadWindowViewModel>();
        var startWindow = _windowManager.Show(viewModel);

        if (startWindow is not Window window)
        {
            throw new NotImplementedException();
        }

        window.DataContext = viewModel;

        return window;
    }

    public IWindowViewModel ChangeViewModelByRememberMe(bool IsRememberMe)
        => IsRememberMe ? _container.Resolve<IMenuWindowViewModel>() : _container.Resolve<IMainWindowViewModel>();

    private void InitializeDependencies()
    {
        _container.Resolve<IPathServiceInitializer>().Initialize();
        _container.Resolve<IWindowDataMementoWrapperInitializer>().Initialize();
        _container.Resolve<ISettingSizeInitialize>().Initialize();
        _container.Resolve<IUrlServiceInitializer>().Initialize();
        _container.Resolve<IAvatarServiceInitializer>().Initialize();
        _container.Resolve<IUserDataMementoWrapperInitializer>().Initialize();
        _container.Resolve<ICurrentUserServiceInitializer>().Initialize();
        _container.Resolve<IProfilePageServiceInitializer>().Initialize();
        _container.Resolve<ISettingsPageServiceInitializer>().Initialize();
    }

    public void Dispose() => _container.Dispose();
}