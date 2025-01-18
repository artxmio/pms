using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.SettingSize;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Pages;
using ProjectManagementStudio.ViewModel.PageServices.IProfilePageService;
using ProjectManagementStudio.ViewModel.SettingSizeService;
using ProjectManagementStudio.ViewModel.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ProjectManagementStudio.ViewModel.MenuWindow;

public class MenuWindowViewModel : IMenuWindowViewModel, INotifyPropertyChanged
{
    /* // Поля // */
    #region

    private readonly IPageManager _pageManager;
    private readonly IWindowManager _windowManager;

    private readonly ICurrentUserService _currentUserService;
    private readonly IProfilePageService _profilePageService;
    private IPage _activePage;
    private BitmapImage _avatarImage = new();
    private readonly ISettingSizeService _settingSizeService;

    #endregion

    /* // Свойства // */
    #region

    public ICurrentUserModel CurrentUser
    {
        get
        {
            return _currentUserService.CurrentUser;
        }
        set
        {
            _currentUserService.CurrentUser = value;

            OnPropertyChanged();
        }
    }

    public IPage ActivePage
    {
        get => _activePage;
        set
        {
            _activePage = value;
            OnPropertyChanged();
        }
    }

    public BitmapImage AvatarImage
    {
        get => _avatarImage;
        set
        {
            if (value is not null)
            {
                _avatarImage = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<IWindowSizes> Sizes
    {
        get
        {
            return _settingSizeService.Sizes;
        }
    }

    public IWindowSizes Current
    {
        get
        {
            return _settingSizeService.Current;
        }
        set
        {
            _settingSizeService.Current = value;
            OnPropertyChanged();
        }
    }

    #endregion

    /* // Команды // */
    #region 

    public ICommand CloseCommand { get; }
    public ICommand RollCommand { get; }
    public ICommand RestoreCommand { get; }

    public ICommand NavigateToProfilePage { get; }
    public ICommand NavigateToWelcomePage { get; }
    public ICommand NavigateToSettingsPage { get; }

    public ICommand ChangeAvatarCommand { get; }
    public ICommand ChangeLoginCommand { get; }
    public ICommand ChangePasswordCommand { get; }
    public ICommand ChangeEmailCommand { get; }
    public ICommand ChangeAboutTextCommand { get; }

    public ICommand ApplySettings { get; }

    public ICommand LogOutCommand { get; }

    #endregion

    public MenuWindowViewModel(
        IWindowManager windowManager,
        IPageManager pageManager,
        ICurrentUserService currentUserService,
        IProfilePageService profilePageService,
        ISettingSizeService settingSizeService)
    {
        _pageManager = pageManager;
        _windowManager = windowManager;
        _currentUserService = currentUserService;

        _profilePageService = profilePageService;

        _activePage = _pageManager.NavigateTo(2);

        AvatarImage = profilePageService.AvatarImage;

        CloseCommand = new RelayCommand(o => CloseWindow());
        RollCommand = new RelayCommand(o => RollWindow(o));
        RestoreCommand = new RelayCommand(o => RestoreWindow(o));

        NavigateToWelcomePage = new RelayCommand(o => ActivePage = _pageManager.NavigateTo(2));
        NavigateToProfilePage = new RelayCommand(o => ActivePage = _pageManager.NavigateTo(3));
        NavigateToSettingsPage = new RelayCommand(o => ActivePage = _pageManager.NavigateTo(4));

        // Profile's functions //
        #region 
        ChangeAvatarCommand = new RelayCommand(o => AvatarImage = _profilePageService.ChangeAvatar());
        ChangeLoginCommand = new RelayCommand(o => _profilePageService.OpenChangeLoginWindow());
        ChangePasswordCommand = new RelayCommand(o => _profilePageService.OpenChangePasswordWindow());
        ChangeEmailCommand = new RelayCommand(o => _profilePageService.OpenChangeEmailWindow());
        ChangeAboutTextCommand = new RelayCommand(o => _profilePageService.OpenChangeAboutTextWindow());

        LogOutCommand = new RelayCommand(o => _profilePageService.Logout(this));
        #endregion

        // Setting's functions //
        #region

        _settingSizeService = settingSizeService;
        ApplySettings = new RelayCommand(o => _settingSizeService.ApplySettings());
        #endregion
    }

    private void CloseWindow()
    {
        _windowManager.Close(this);
    }

    private static void RollWindow(object parametr)
    {
        if (parametr is Window window)
        {
            window.WindowState = WindowState.Minimized;
        }
    }

    private static void RestoreWindow(object parametr)
    {
        if (parametr is Window window)
        {
            if (window.WindowState != WindowState.Maximized)
            {
                window.WindowState = WindowState.Maximized;
            }
            else
            {
                window.WindowState = WindowState.Normal;
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}