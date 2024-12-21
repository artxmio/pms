using Microsoft.Win32;
using ProjectManagementStudio.Bootstrapper.Services.AvatarService;
using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.MenuWindowModels.ProfileModel;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows;
using ProjectManagementStudio.ViewModel.Pages;
using ProjectManagementStudio.ViewModel.Windows;
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

    private readonly IAPIClient _client;
    private readonly IPageManager _pageManager;
    private readonly IWindowManager _windowManager;

    private readonly IAvatarService _avatarService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserDataMementoWrapper _userDataMementoWrapper;
    private readonly IChangeLoginModalWindowViewModel _changeLoginModalWindowViewModel;
    private IPage _activePage;
    private BitmapImage _avatarImage;
    #endregion

    /* // Свойства // */
    #region

    public IProfileModel ProfileModel { get; set; }

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

    /* // Команды //*/
    #region 

    public ICommand CloseCommand { get; }

    public ICommand NavigateToProfilePage { get; }
    public ICommand NavigateToWelcomePage { get; }
    public ICommand NavigateToSettingsPage { get; }

    public ICommand ChangeAvatarCommand { get; }
    public ICommand ChangeLoginCommand { get; }
    public ICommand ChangePasswordCommand { get; }
    public ICommand ChangeEmailCommand { get; }

    public ICommand LogOutCommand { get; }

    #endregion

    public MenuWindowViewModel(
        IAPIClient APIClient,
        IWindowManager windowManager,
        IPageManager pageManager,
        IProfileModel profileModel,
        IAvatarService avatarService,
        ICurrentUserService currentUserService,
        IUserDataMementoWrapper userDataMementoWrapper,
        IChangeLoginModalWindowViewModel changeLoginModalWindowViewModel)
    {
        _client = APIClient;
        _pageManager = pageManager;
        _windowManager = windowManager;
        _avatarService = avatarService;
        _currentUserService = currentUserService;
        _userDataMementoWrapper = userDataMementoWrapper;
        _changeLoginModalWindowViewModel = changeLoginModalWindowViewModel;

        ProfileModel = profileModel;

        _activePage = _pageManager.NavigateTo(2);

        /* // Загрузка аватарки // */
        _avatarImage = new BitmapImage();
        _avatarImage.BeginInit();
        _avatarImage.UriSource = new Uri(_avatarService.AvatarFilePath);
        _avatarImage.CacheOption = BitmapCacheOption.OnLoad;
        _avatarImage.EndInit();

        CloseCommand = new RelayCommand(o => _windowManager.Close(this));
        NavigateToWelcomePage = new RelayCommand(o => ActivePage = _pageManager.NavigateTo(2));
        NavigateToProfilePage = new RelayCommand(o => ActivePage = _pageManager.NavigateTo(3));
        NavigateToSettingsPage = new RelayCommand(o => ActivePage = _pageManager.NavigateTo(4));

        ChangeAvatarCommand = new RelayCommand(o => ChangeAvatar());
        ChangeLoginCommand = new RelayCommand(o => ChangeLogin());
        ChangePasswordCommand = new AsyncCommand(ChangePassword);
        ChangeEmailCommand = new AsyncCommand(ChangeEmail);

        LogOutCommand = new RelayCommand(o => LogOut());
    }

    private void ChangeAvatar()
    {
        OpenFileDialog openFileDialog = new()
        {
            Title = "Выберите аватар",
            InitialDirectory = "c:\\",
            Filter = "Image files (*.png;*.jpg)|*.png;*.jpg",
            FilterIndex = 2
        };

        if (openFileDialog.ShowDialog() is not null && openFileDialog.FileName != string.Empty)
        {
            AvatarImage = _avatarService.ChangeAvatar(openFileDialog.FileName);
        }
    }

    // доделать попозже
    private void ChangeLogin()
    {
        var dialogWindow = _windowManager.Show(_changeLoginModalWindowViewModel, true) as Window;

        if (dialogWindow is not null)
        {
            dialogWindow.DataContext = _changeLoginModalWindowViewModel;
        }
        
        
    }
    private async Task ChangePassword()
    {
        //var dialogWindow = _windowManager.Show(_passwordChangeDialogViewModel, true);

        //if (dialogWindow is not Window window)
        //{
        //    throw new NotImplementedException();
        //}

        //if (window.DialogResult == true)
        //    await _client.ChangePassword(CurrentUser, "password123");
    }
    private async Task ChangeEmail()
    {
        //var dialogWindow = _windowManager.Show(_emailChangeDialogViewModel, true);

        //if (dialogWindow is not Window window)
        //{
        //    throw new NotImplementedException();
        //}

        //if (window.DialogResult == true)
        //    await _client.ChangeEmail(CurrentUser, "email321@gmail.com");
    }
    //////////////////////////////////////

    private void LogOut()
    {
        //надо сделать собственное окно вот с таким выбором
        var result = MessageBox.Show("Нажмите 'да', чтобы выйти из аккаунта", "Внимание", MessageBoxButton.YesNo);

        if (result == MessageBoxResult.Yes)
        {
            _userDataMementoWrapper.DeleteUserData();
            _windowManager.Close(this);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}