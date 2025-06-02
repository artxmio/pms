using Autofac;
using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Bootstrapper.Services.PageServices.ProfilePageService;
using ProjectManagementStudio.Bootstrapper.Services.PageServices.ProjectsPageService;
using ProjectManagementStudio.Bootstrapper.Services.PageServices.SettingsPageService;
using ProjectManagementStudio.Bootstrapper.Services.PathService;
using ProjectManagementStudio.Bootstrapper.Services.UrlService;
using ProjectManagementStudio.Bootstrapper.ThemeService;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.ViewModel.LocalizationService;
using ProjectManagementStudio.ViewModel.MainWindow;
using ProjectManagementStudio.ViewModel.MenuWindow;
using ProjectManagementStudio.ViewModel.ThemeService;
using ProjectManagementStudio.ViewModel.Windows;
using System.Windows;

namespace ProjectManagementStudio.Bootstrapper;

public class Bootstrapper : IDisposable
{
    private readonly IContainer _container;
    private readonly IWindowManager _windowManager;
    private readonly ILocalizationService _localizationService;

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
        _localizationService = _container.Resolve<ILocalizationService>();
    }

    public Window Run()
    {
        InitializeDependencies();

        var _userDataMementoWrapper = _container.Resolve<IUserDataMementoWrapper>();

        var viewModel = ChangeViewModelByRememberMe(_userDataMementoWrapper.IsRememberMe);

        IWindow startWindow;

        if (viewModel is IMainWindowViewModel mainWindowViewModel)
        {
            startWindow = _windowManager.Show(mainWindowViewModel);
        }
        else if (viewModel is IMenuWindowViewModel menuWindowViewModel)
        {
            startWindow = _windowManager.Show(menuWindowViewModel);
        }
        else
        {
            throw new InvalidOperationException("Unknown ViewModel type");
        }

        if (startWindow is not Window window)
        {
            throw new NotImplementedException();
        }

        window.DataContext = viewModel;

        var styles = new ResourceDictionary()
        {
            Source = new Uri("pack://application:,,,/ProjectManagementStudio.View;component/Themes/GreenTheme.xaml", UriKind.RelativeOrAbsolute)
        };
        Application.Current.Resources.MergedDictionaries.Add(styles);
        return window;
    }

    public IWindowViewModel ChangeViewModelByRememberMe(bool IsRememberMe)
        => IsRememberMe ? _container.Resolve<IMenuWindowViewModel>() : _container.Resolve<IMainWindowViewModel>();

    private void InitializeDependencies()
    {
        _container.Resolve<IPathServiceInitializer>().Initialize();
        _container.Resolve<IUrlServiceInitializer>().Initialize();
        _container.Resolve<IUserDataMementoWrapperInitializer>().Initialize();
        _container.Resolve<ICurrentUserServiceInitializer>().Initialize();
        _container.Resolve<ISettingsPageServiceInitializer>().Initialize();
        _container.Resolve<IProjectsPageServiceInitializer>().Initialize();
        _container.Resolve<IThemeServiceInitializer>().Initialize();
    }

    public void Dispose() => _container.Dispose();
}