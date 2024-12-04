using Microsoft.Win32;
using ProjectManagementStudio.Bootstrapper.Services.AvatarService;
using ProjectManagementStudio.Model.MenuWindowModels.ProfileModel;
using ProjectManagementStudio.Model.PathService;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Pages;
using ProjectManagementStudio.ViewModel.Windows;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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

    private readonly IPathService _pathService;
    private readonly IAvatarService _avatarService;

    private IPage _activePage;

    #endregion

    /* // Свойства // */
    #region

    public IProfileModel ProfileModel { get; set; }

    public IPage ActivePage
    {
        get => _activePage;
        set
        {
            _activePage = value;
            OnPropertyChanged();
        }
    }

    private BitmapImage _avatarImage;
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

    #endregion

    public MenuWindowViewModel(
        IWindowManager windowManager,
        IPageManager pageManager,
        IProfileModel profileModel,
        IPathService pathService,
        IAvatarService avatarService)
    {
        _pageManager = pageManager;
        _windowManager = windowManager;
        _pathService = pathService;
        _avatarService = avatarService;

        ProfileModel = profileModel;

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