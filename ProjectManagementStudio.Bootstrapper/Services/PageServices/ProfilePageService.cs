using Microsoft.Win32;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.ViewModel.AvatarService;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.NewEmail;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.NewLogin;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.NewPassword;
using ProjectManagementStudio.ViewModel.PageServices.IProfilePageService;
using ProjectManagementStudio.ViewModel.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProjectManagementStudio.Bootstrapper.Services.PageServices;

internal class ProfilePageService : IProfilePageService, IProfilePageServiceInitializer, INotifyPropertyChanged
{
    private bool _initialized;
    private readonly IWindowManager _windowManager;
    private readonly IAvatarService _avatarService;
    private readonly IUserDataMementoWrapper _userDataMementoWrapper;
    private readonly IChangeLoginModalWindowViewModel _changeLoginModalWindowViewModel;
    private readonly IChangePasswordModalWindowViewModel _changePasswordModalWindowViewModel;
    private readonly IChangeEmailModalWindowViewModel _changeEmailModalWindowViewModel;

    private BitmapImage? _avatarImage;

    public BitmapImage AvatarImage
    {
        get
        {
            EnsureInitialized();
            return _avatarImage ?? throw new NullReferenceException();
        }
        set
        {
            if (value is not null)
            {
                _avatarImage = value;
                OnPropertyChanged();
            }
        }
    }

    public ProfilePageService(
        IWindowManager windowManager,
        IAvatarService avatarService,
        IUserDataMementoWrapper userDataMementoWrapper,
        IChangeLoginModalWindowViewModel changeLoginModalWindowViewModel,
        IChangePasswordModalWindowViewModel changePasswordModalWindowViewModel,
        IChangeEmailModalWindowViewModel changeEmailModalWindowViewModel)
    {
        _windowManager = windowManager;
        _avatarService = avatarService;
        _userDataMementoWrapper = userDataMementoWrapper;

        _changeLoginModalWindowViewModel = changeLoginModalWindowViewModel;
        _changePasswordModalWindowViewModel = changePasswordModalWindowViewModel;
        _changeEmailModalWindowViewModel = changeEmailModalWindowViewModel;
    }

    public void Initialize()
    {
        if (_initialized)
        {
            throw new InvalidOperationException($"{nameof(IProfilePageService)} is already initialized");
        }

        _initialized = true;

        //Initialize avatar image
        _avatarImage = new BitmapImage();
        _avatarImage.BeginInit();
        _avatarImage.UriSource = new Uri(_avatarService.AvatarFilePath);
        _avatarImage.CacheOption = BitmapCacheOption.OnLoad;
        _avatarImage.EndInit();
    }

    public BitmapImage ChangeAvatar()
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

        return AvatarImage ?? throw new NullReferenceException("Avatar Image is null");
    }

    public void OpenChangeLoginWindow()
    {
        _windowManager.Show(_changeLoginModalWindowViewModel, true);
    }

    public void OpenChangePasswordWindow()
    {
        _windowManager.Show(_changePasswordModalWindowViewModel, true);
    }

    public void OpenChangeEmailWindow()
    {
        _windowManager.Show(_changeEmailModalWindowViewModel, true);
    }

    public void Logout(IWindowViewModel viewModel)
    {
        var result = MessageBox.Show("Нажмите 'да', чтобы выйти из аккаунта", "Внимание", MessageBoxButton.YesNo);

        if (result == MessageBoxResult.Yes)
        {
            _userDataMementoWrapper.DeleteUserData();
            _windowManager.Close(viewModel);
        }
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"{nameof(IAvatarService)} is not initialized");
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
