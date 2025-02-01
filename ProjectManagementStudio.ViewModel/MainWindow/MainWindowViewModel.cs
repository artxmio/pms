using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Model.WindowModels.AuthModel;
using ProjectManagementStudio.Model.WindowModels.RegisterModel;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.LocalizationService;
using ProjectManagementStudio.ViewModel.MenuWindow;
using ProjectManagementStudio.ViewModel.Pages;
using ProjectManagementStudio.ViewModel.Windows;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.MainWindow;

public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
{
    /* // Поля // */
    #region

    private readonly IAPIClient _client;
    private readonly IPageManager _pageManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILocalizationService _localizationService;
    private readonly IMenuWindowViewModel _menuWindowViewModel;
    private readonly IWindowManager _windowManager;

    private IPage _activePage;

    #endregion

    /* // Модели // */
    #region

    public IAuthModel LoginModel { get; set; }
    public IRegisterModel RegistrationModel { get; set; }

    #endregion

    /* // Команды // */
    #region

    public ICommand CloseCommand { get; }
    public ICommand AuthorizationCommand { get; }
    public ICommand RegistrationCommand { get; }

    public ICommand ChangeLocalizationCommand { get; }

    public ICommand NavigateToRegistrationPage { get; }
    public ICommand NavigateToLoginPage { get; }

    #endregion

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
        IPageManager pageManager,
        IAuthModel authModel,
        IRegisterModel registerModel,
        ICurrentUserService currentUserService,
        ILocalizationService localizationService
        )
    {
        LoginModel = authModel;
        RegistrationModel = registerModel;

        _menuWindowViewModel = menuWindowViewModel;
        _windowManager = windowManager;
        _client = apiClient;
        _pageManager = pageManager;
        _currentUserService = currentUserService;
        
        _localizationService = localizationService;
        _localizationService.LanguageChanged += LanguageChanged;

        _activePage = _pageManager.NavigateTo(0);

        CloseCommand = new RelayCommand(o => _windowManager.Close(this));
        AuthorizationCommand = new RelayCommand(o => AuthorizateUser());
        RegistrationCommand = new AsyncCommand(() => _client.AddUser(RegistrationModel));

        ChangeLocalizationCommand = new RelayCommand(o => _localizationService.Language = new CultureInfo((string)o));

        NavigateToRegistrationPage = new RelayCommand(o => ActivePage = _pageManager.NavigateTo(1));
        NavigateToLoginPage = new RelayCommand(o => ActivePage = _pageManager.NavigateTo(0));
    }

    private void LanguageChanged(object? sender, EventArgs e)
    {
        
    }

    private async void AuthorizateUser()
    {
        LoginModel.IsValid = false;
        
        bool isExist = await _client.IsUserExists(LoginModel.Login, LoginModel.Password);

        if (isExist)
        {
            _currentUserService.CurrentUser = await _client.GetUserByLogin(LoginModel);
            var menuWindow = _windowManager.Show(_menuWindowViewModel) as Window;

            if (menuWindow is not Window window)
            {
                throw new NotImplementedException();
            }
            window.DataContext = _menuWindowViewModel;

            _windowManager.Close(this);
        }
        else
        {
            MessageBox.Show("Такого пользователя не существует или возникла неизвестная ошибка", "Ошибка");
            LoginModel.IsValid = true;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}