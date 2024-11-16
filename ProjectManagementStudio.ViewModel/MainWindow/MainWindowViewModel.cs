using ProjectManagementStudio.ViewModel.Windows;
using ProjectManagementStudio.ViewModel.Command;
using System.Windows.Input;
using ProjectManagementStudio.ViewModel.ValidationsRules;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.Model.WindowModels.RegisterModel;
using ProjectManagementStudio.Model.WindowModels.AuthModel;

namespace ProjectManagementStudio.ViewModel.MainWindow;

public class MainWindowViewModel : IMainWindowViewModel
{
    private readonly IWindowManager _windowManager;
    private readonly APIClient.APIClient _client;

    public AuthModel LoginModel { get; set; }
    public RegisterModel RegistrationModel { get; set; }

    public ICommand CloseCommand { get; }
    public ICommand AuthorizationCommand { get; }

    public MainWindowViewModel(
        IWindowManager windowManager, 
        IUserDataMementoWrapper userDataMementoWrapper)
    {
        LoginModel = new AuthModel(userDataMementoWrapper);
        RegistrationModel = new RegisterModel();

        _windowManager = windowManager;
        _client = new APIClient.APIClient();

        CloseCommand = new RelayCommand(() => _windowManager.Close(this));
        AuthorizationCommand = new AsyncCommand(() => _client.IsUserExists(LoginModel));
    }
}