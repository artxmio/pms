using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.MainWindow;
using ProjectManagementStudio.ViewModel.MenuWindow;
using ProjectManagementStudio.ViewModel.Windows;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.PreloadWindow;

public class PreloadWindowViewModel : IPreloadWindowViewModel
{
    private readonly IUserDataMementoWrapper _userDataMementoWrapper;
    private readonly IWindowManager _windowManager;
    private readonly IMainWindowViewModel _mainWindowViewModel;
    private readonly IMenuWindowViewModel _menuWindowViewModel;

    public ICommand ContinueCommand { get; set; }
    public bool IsAvaibleInternetConnection { get; set; }

    public PreloadWindowViewModel(IUserDataMementoWrapper userDataMementoWrapper,
        IWindowManager windowManager,
        IMainWindowViewModel mainWindowViewModel,
        IMenuWindowViewModel menuWindowViewModel)
    {
        _userDataMementoWrapper = userDataMementoWrapper;
        _windowManager = windowManager;
        _mainWindowViewModel = mainWindowViewModel;
        _menuWindowViewModel = menuWindowViewModel;

        ContinueCommand = new RelayCommand(o => ShowWindow());
    }

    static async Task<bool> CheckInternetConnection()
    {
        try
        {
            using HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(3);
            HttpResponseMessage response = await client.GetAsync("http://www.google.com");
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task ShowWindow()
    { 
        IsAvaibleInternetConnection = await CheckInternetConnection();

        if (!IsAvaibleInternetConnection)
        {
            MessageBox.Show("Отсутствует интернет-подключение", "Ошибка");
            return;
        }

        var viewModel = ChangeViewModelByRememberMe(_userDataMementoWrapper.IsRememberMe);

        IWindow nextWindow;

        if (viewModel is IMainWindowViewModel mainWindowViewModel)
        {
            nextWindow = _windowManager.Show(mainWindowViewModel);
        }
        else if (viewModel is IMenuWindowViewModel menuWindowViewModel)
        {
            nextWindow = _windowManager.Show(menuWindowViewModel);
        }
        else
        {
            throw new InvalidOperationException("Unknown ViewModel type");
        }
        
        _windowManager.Close(this);

        if (nextWindow is not Window window)
        {
            throw new NotImplementedException();
        }

        window.DataContext = viewModel;
    }

    private IWindowViewModel ChangeViewModelByRememberMe(bool IsRememberMe)
        => IsRememberMe ? _menuWindowViewModel : _mainWindowViewModel;
}