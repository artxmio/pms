using Autofac;
using ProjectManagementStudio.ViewModel.MainWindow;
using ProjectManagementStudio.ViewModel.Windows;
using System.Windows;

namespace ProjectManagementStudio.Bootstrapper;

/*
    Класс отвечает за запуск программы и регистрацию всех модулей
    в контейнере.
*/

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
        var mainWindowViewModel = _container.Resolve<IMainWindowViewModel>();

        var mainWindow = _windowManager.Show(mainWindowViewModel);

        if (mainWindow is not Window window)
        {
            throw new NotImplementedException();
        }

        window.DataContext = mainWindowViewModel;

        return window;
    }

    public void Dispose()
    {
        _container.Dispose();
    }
}