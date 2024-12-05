using Microsoft.Win32;
using ProjectManagementStudio.Bootstrapper.Services.AvatarService;
using ProjectManagementStudio.Model.MenuWindowModels.ProfileModel;
using ProjectManagementStudio.Model.PathService;
using ProjectManagementStudio.Model.WindowModels.AuthModel;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Pages;
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

    private readonly IAPIClient _client;
    private readonly IPageManager _pageManager;
    private readonly IWindowManager _windowManager;

    private readonly IAvatarService _avatarService;

    private IPage _activePage;

    private BitmapImage _avatarImage;
    #endregion

    /* // Свойства // */
    #region

    public IProfileModel ProfileModel { get; set; }
    public IAuthModel CurrentUser { get; set; }

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

    #endregion

    public MenuWindowViewModel(
        IAPIClient APIClient,
        IWindowManager windowManager,
        IPageManager pageManager,
        IProfileModel profileModel,
        IAvatarService avatarService,
        IAuthModel currentUser)
    {
        _client = APIClient;
        _pageManager = pageManager;
        _windowManager = windowManager;
        _avatarService = avatarService;

        ProfileModel = profileModel;
        CurrentUser = currentUser;

        _activePage = _pageManager.NavigateTo(2);

        /* // Загрузка аватарки // */
        _avatarImage = new BitmapImage();
        _avatarImage.BeginInit();
        _avatarImage.UriSource = new Uri(_avatarService.AvatarFilePath);
        _avatarImage.CacheOption = BitmapCacheOption.OnLoad;
        _avatarImage.EndInit();

        CloseCommand = new RelayCommand(() => _windowManager.Close(this));
        NavigateToWelcomePage = new RelayCommand(() => ActivePage = _pageManager.NavigateTo(2));
        NavigateToProfilePage = new RelayCommand(() => ActivePage = _pageManager.NavigateTo(3));
        NavigateToSettingsPage = new RelayCommand(() => ActivePage = _pageManager.NavigateTo(4));
        ChangeAvatarCommand = new RelayCommand(ChangeAvatar);
        ChangeLoginCommand = new RelayCommand(() => _client.GetUserByLogin(CurrentUser));
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

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}