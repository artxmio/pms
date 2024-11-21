using ProjectManagementStudio.ViewModel.Windows;
using ProjectManagementStudio.ViewModel.Command;
using System.Windows.Input;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.Model.WindowModels.RegisterModel;
using ProjectManagementStudio.Model.WindowModels.AuthModel;
using ProjectManagementStudio.ViewModel.MenuWindow;
using ProjectManagementStudio.ViewModel.APIClient;
using System.Windows;
using ProjectManagementStudio.ViewModel.Pages;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectManagementStudio.ViewModel.MainWindow;

public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
{
    private readonly IAPIClient _client;
    private readonly IPageManager _pageManager;
    private readonly IMenuWindowViewModel _menuWindowViewModel;
    private readonly IWindowManager _windowManager;

    #region
    public AuthModel LoginModel { get; set; }
    public RegisterModel RegistrationModel { get; set; }
    #endregion

    #region
    public ICommand CloseCommand { get; }
    public ICommand AuthorizationCommand { get; }
    public ICommand RegistrationCommand { get; }

    public ICommand NavigateToRegistrationPage { get; }
    public ICommand NavigateToLoginPage { get; }
    #endregion

    private IPage _activePage;

    public event PropertyChangedEventHandler? PropertyChanged;

    public IPage ActivePage
    {
        get => _activePage;
        set
        {
            _activePage = value;
            OnPropertyChanged();
        }
    }

    public MainWindowViewModel(
        IAPIClient apiClient,
        IMenuWindowViewModel menuWindowViewModel,
        IWindowManager windowManager,
        IUserDataMementoWrapper userDataMementoWrapper,
        IPageManager pageManager
        )
    {
        LoginModel = new AuthModel(userDataMementoWrapper);
        RegistrationModel = new RegisterModel();

        _menuWindowViewModel = menuWindowViewModel;
        _windowManager = windowManager;
        _client = apiClient;
        _pageManager = pageManager;

        _activePage = _pageManager.ActivePage;

        CloseCommand = new RelayCommand(() => _windowManager.Close(this));
        AuthorizationCommand = new RelayCommand(AuthorizateUser);
        RegistrationCommand = new AsyncCommand(() => _client.AddUser(RegistrationModel));

        NavigateToRegistrationPage = new RelayCommand(() => ActivePage = _pageManager.NavigateTo(1));
        NavigateToLoginPage = new RelayCommand(() => ActivePage = _pageManager.NavigateTo(0));
    }

    private async void AuthorizateUser()
    {
        bool isExist = await _client.IsUserExists(LoginModel);
        if (isExist)
        {
            _windowManager.Show(_menuWindowViewModel);
            _windowManager.Close(this);
        }
        else
        {
            MessageBox.Show("Такого пользователя не существует", "Упс!");
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}