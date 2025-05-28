using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Model.MenuWindowModels.ProfileModel.ModalWindowModels.AboutText;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.APIClient.Enums;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.AboutText;

public class ChangeAboutTextModalWindowViewModel : IChangeAboutTextModalWindowViewModel, INotifyPropertyChanged
{
    private readonly IUserDataMementoWrapper _userDataMementoWrapper;
    private readonly ICurrentUserService _currentUserService;
    private readonly IWindowManager _windowManager;
    private readonly IAPIClient _client;
    private IAboutTextModel _model;

    public string AboutText
    {
        get => _model.AboutText;
        set
        {
            _model.AboutText = value;
            OnPropertyChanged();
        }
    }

    public ICommand CloseCommand { get; }
    public ICommand ChangeAboutTextCommand { get; }

    public ChangeAboutTextModalWindowViewModel(
        IWindowManager windowManager,
        IUserDataMementoWrapper userDataMementoWrapper,
        ICurrentUserService currentUserService,
        IAPIClient client,
        IAboutTextModel model)
    {
        _userDataMementoWrapper = userDataMementoWrapper;
        _currentUserService = currentUserService;
        _windowManager = windowManager;
        _client = client;

        _model = model;

        model.AboutText = _userDataMementoWrapper.AboutText;
        _currentUserService.CurrentUser.AboutText = _model.AboutText;

        CloseCommand = new RelayCommand(o => CloseDialog());
        ChangeAboutTextCommand = new RelayCommand(o => ChangeAboutText());
    }

    private async void ChangeAboutText()
    {
        await _client.ChangeUserParametr(_currentUserService.CurrentUser.UserId, ChangeableParams.AboutText, AboutText);

        _currentUserService.CurrentUser.AboutText = AboutText;
        _userDataMementoWrapper.AboutText = AboutText;
        _userDataMementoWrapper.SaveUserData();

        _windowManager.Close(this);
    }

    private void CloseDialog()
    {
        _windowManager.Close(this);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
