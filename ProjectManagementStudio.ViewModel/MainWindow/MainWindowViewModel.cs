using ProjectManagementStudio.ViewModel.Windows;
using ProjectManagementStudio.ViewModel.Command;
using System.Windows.Input;
using ProjectManagementStudio.Model.AuthModel;
using ProjectManagementStudio.ViewModel.ValidationsRules;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;

namespace ProjectManagementStudio.ViewModel.MainWindow;

public class MainWindowViewModel : IMainWindowViewModel
{
    private readonly IWindowManager _windowManager;
    private readonly APIClient.APIClient _client;

    public AuthModel Model { get; set; }

    public ICommand CloseCommand { get; }
    public ICommand AuthorizationCommand { get; }

    public MainWindowViewModel(
        IWindowManager windowManager, 
        IUserDataMementoWrapper userDataMementoWrapper)
    {
        Model = new AuthModel(userDataMementoWrapper);

        _windowManager = windowManager;
        _client = new APIClient.APIClient();

        CloseCommand = new RelayCommand(() => _windowManager.Close(this));
        AuthorizationCommand = new AsyncCommand(() => _client.IsUserExists(Model));
    }
}