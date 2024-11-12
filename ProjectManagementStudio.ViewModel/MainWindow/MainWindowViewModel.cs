using ProjectManagementStudio.ViewModel.Windows;
using ProjectManagementStudio.ViewModel.Command;
using System.Windows.Input;
using ProjectManagementStudio.Model.AuthModel;
using ProjectManagementStudio.ViewModel.ValidationsRules;

namespace ProjectManagementStudio.ViewModel.MainWindow;

public class MainWindowViewModel : IMainWindowViewModel
{
    private readonly IWindowManager _windowManager;
    private readonly APIClient.APIClient _client;

    public AuthModel Model { get; set; }

    public ICommand CloseCommand { get; }
    public ICommand AuthorizationCommand { get; }

    public MainWindowViewModel(IWindowManager windowManager)
    {
        Model = new AuthModel();

        _windowManager = windowManager;
        _client = new APIClient.APIClient();

        CloseCommand = new RelayCommand(() => _windowManager.Close(this));
        AuthorizationCommand = new AsyncCommand(() => _client.IsUserExists(Model));
    }
}