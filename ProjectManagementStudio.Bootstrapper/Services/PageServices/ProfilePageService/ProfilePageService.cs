using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.AboutText;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.NewEmail;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.NewLogin;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.NewPassword;
using ProjectManagementStudio.ViewModel.PageServices.IProfilePageService;
using ProjectManagementStudio.ViewModel.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ProjectManagementStudio.Bootstrapper.Services.PageServices.ProfilePageService;

internal class ProfilePageService : IProfilePageService, IProfilePageServiceInitializer, INotifyPropertyChanged
{
    private bool _initialized;
    private readonly IWindowManager _windowManager;
    private readonly IUserDataMementoWrapper _userDataMementoWrapper;
    private readonly IChangeLoginModalWindowViewModel _changeLoginModalWindowViewModel;
    private readonly IChangePasswordModalWindowViewModel _changePasswordModalWindowViewModel;
    private readonly IChangeEmailModalWindowViewModel _changeEmailModalWindowViewModel;
    private readonly IChangeAboutTextModalWindowViewModel _changeAboutTextModalWindowViewModel;

    public string AboutText
    {
        get
        {
            EnsureInitialized();
            return _userDataMementoWrapper.AboutText ?? throw new NullReferenceException();
        }
        set
        {
            if (value is not null)
            {
                _userDataMementoWrapper.AboutText = value;
                OnPropertyChanged();
            }
        }
    }

    public ProfilePageService(
        IWindowManager windowManager,
        IUserDataMementoWrapper userDataMementoWrapper,
        IChangeLoginModalWindowViewModel changeLoginModalWindowViewModel,
        IChangePasswordModalWindowViewModel changePasswordModalWindowViewModel,
        IChangeEmailModalWindowViewModel changeEmailModalWindowViewModel,
        IChangeAboutTextModalWindowViewModel changeAboutTextModalWindowViewModel)
    {
        _windowManager = windowManager;
        _userDataMementoWrapper = userDataMementoWrapper;

        _changeLoginModalWindowViewModel = changeLoginModalWindowViewModel;
        _changePasswordModalWindowViewModel = changePasswordModalWindowViewModel;
        _changeEmailModalWindowViewModel = changeEmailModalWindowViewModel;
        _changeAboutTextModalWindowViewModel = changeAboutTextModalWindowViewModel;
    }

    public void Initialize()
    {
        if (_initialized)
        {
            throw new InvalidOperationException($"{nameof(IProfilePageService)} is already initialized");
        }

        _initialized = true;
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

    public void OpenChangeAboutTextWindow()
    {
        _windowManager.Show(_changeAboutTextModalWindowViewModel, true);
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
            throw new InvalidOperationException($"{nameof(IProfilePageService)} is not initialized");
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
