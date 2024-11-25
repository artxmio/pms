using Autofac;
using ProjectManagementStudio.Bootstrapper.Services.PathService;
using ProjectManagementStudio.Bootstrapper.Services.UrlService;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.ViewModel.MainWindow;
using ProjectManagementStudio.ViewModel.MenuWindow;
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

        var mainWindowViewModel = _container.Resolve<IMainWindowViewModel>();

        var mainWindow = _windowManager.Show(mainWindowViewModel);

        if (mainWindow is not Window window)
        {
            throw new NotImplementedException();
        }

        window.DataContext = mainWindowViewModel;

        return window;
    }

    private void InitializeDependencies()
    {
        _container.Resolve<IUrlServiceInitializer>().Initialize();
        _container.Resolve<IPathServiceInitializer>().Initialize();
        _container.Resolve<IUserDataMementoWrapperInitializer>().Initialize();
    }

    public void Dispose() => GC.SuppressFinalize(_container);
}