using ProjectManagementStudio.ViewModel.Windows;
using ProjectManagementStudio.ViewModel.Command;
using System.Windows.Input;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.Model.WindowModels.RegisterModel;
using ProjectManagementStudio.Model.WindowModels.AuthModel;
using ProjectManagementStudio.ViewModel.MenuWindow;
using ProjectManagementStudio.ViewModel.APIClient;
using System.Windows;

namespace ProjectManagementStudio.ViewModel.MainWindow;

public class MainWindowViewModel : IMainWindowViewModel
{
    private readonly IAPIClient _client;
    private readonly IMenuWindowViewModel _menuWindowViewModel;
    private readonly IWindowManager _windowManager;

    #region
    public AuthModel LoginModel { get; set; }
    public RegisterModel RegistrationModel { get; set; }
    #endregion

    #region
    public ICommand CloseCommand { get; }
    public ICommand AuthorizationCommand { get; }
    public ICommand RegistrationCommand { get; }
    #endregion

    public MainWindowViewModel(
        IAPIClient apiClient,
        IMenuWindowViewModel menuWindowViewModel,
        IWindowManager windowManager,
        IUserDataMementoWrapper userDataMementoWrapper
        )
    {
        LoginModel = new AuthModel(userDataMementoWrapper);
        RegistrationModel = new RegisterModel();

        _menuWindowViewModel = menuWindowViewModel;
        _windowManager = windowManager;
        _client = apiClient;

        CloseCommand = new RelayCommand(() => _windowManager.Close(this));
        AuthorizationCommand = new RelayCommand(AuthorizateUser);
        RegistrationCommand = new AsyncCommand(() => _client.AddUser(RegistrationModel));
    }

    private async void AuthorizateUser()
    {
        bool isExist = await _client.IsUserExists(LoginModel);
        if (isExist)
        {
            _windowManager.Show(_menuWindowViewModel);
            _windowManager.Close(this);
        }
        else
        {
            MessageBox.Show("Такого пользователя не существует", "Упс!");
        }
    }
}