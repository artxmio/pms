using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Pages;
using ProjectManagementStudio.ViewModel.PageServices.IProfilePageService;
using ProjectManagementStudio.ViewModel.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
    #endregion

    /* // Команды // */
    #region 

    public ICommand CloseCommand { get; }

    public ICommand NavigateToProfilePage { get; }
    public ICommand NavigateToWelcomePage { get; }
    public ICommand NavigateToSettingsPage { get; }

    public ICommand ChangeAvatarCommand { get; }
    public ICommand ChangeLoginCommand { get; }
    public ICommand ChangePasswordCommand { get; }
    public ICommand ChangeEmailCommand { get; }
    public ICommand ChangeAboutTextCommand { get; }

    public ICommand LogOutCommand { get; }

    #endregion

    public MenuWindowViewModel(
        IWindowManager windowManager,
        IPageManager pageManager,
        ICurrentUserService currentUserService,
        IProfilePageService profilePageService)
    {
        _pageManager = pageManager;
        _windowManager = windowManager;
        _currentUserService = currentUserService;

        _profilePageService = profilePageService;

        _activePage = _pageManager.NavigateTo(2);

        AvatarImage = profilePageService.AvatarImage;

        CloseCommand = new RelayCommand(o => _windowManager.Close(this));
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
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}