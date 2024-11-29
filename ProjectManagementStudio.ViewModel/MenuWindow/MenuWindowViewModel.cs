using Microsoft.Win32;
using ProjectManagementStudio.Model.MenuWindowModels.ProfileModel;
using ProjectManagementStudio.Model.PathService;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Pages;
using ProjectManagementStudio.ViewModel.Windows;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.MenuWindow;

public class MenuWindowViewModel : IMenuWindowViewModel, INotifyPropertyChanged
{
    /* // Поля // */
    #region

    private readonly IPageManager _pageManager;
    private readonly IWindowManager _windowManager;
    private readonly IUserImageMementoWrapper _imageMementoWrapper;
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
    public IUserImageMementoWrapper ImageMementoWrapper
    {
        get => _imageMementoWrapper;
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
        IUserImageMementoWrapper imageMementoWrapper,
        IProfileModel profileModel)
    {
        _pageManager = pageManager;
        _windowManager = windowManager;
        _imageMementoWrapper = imageMementoWrapper;

        ProfileModel = profileModel;

        _activePage = _pageManager.NavigateTo(2);

        CloseCommand = new RelayCommand(() => _windowManager.Close(this));
        NavigateToWelcomePage = new RelayCommand(() => ActivePage = _pageManager.NavigateTo(2));
        NavigateToProfilePage = new RelayCommand(() => ActivePage = _pageManager.NavigateTo(3));
        NavigateToSettingsPage = new RelayCommand(() => ActivePage = _pageManager.NavigateTo(4));
        ChangeAvatarCommand = new RelayCommand(ChangeAvatar);
    }

    private void ChangeAvatar()
    {
        OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            Title = "Выберите аватар",
            InitialDirectory = "c:\\",
            Filter = "Image files (*.png;*.jpg)|*.png;*.jpg",
            FilterIndex = 2
        };

        if(openFileDialog.ShowDialog() is not null)
        {
            //var userDataFolderName = "user";

            //var userDataPath = Path.Combine(_pathService.ApplicationFolder, userDataFolderName);

            //_imageMementoWrapper.AvatarImage = new Uri(openFileDialog.FileName);
            //File.Copy("data\\user.png", , true);
        }

        
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}