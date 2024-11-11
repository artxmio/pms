using ProjectManagementStudio.ViewModel.Windows;
using ProjectManagementStudio.ViewModel.Command;
using System.Windows.Input;
using ProjectManagementStudio.Model.AuthModel;

namespace ProjectManagementStudio.ViewModel.MainWindow;

public class MainWindowViewModel : IMainWindowViewModel
{
    private readonly IWindowManager _windowManager;

    public AuthModel Model { get; set; }

    public ICommand CloseCommand { get; set; }

    public MainWindowViewModel(IWindowManager windowManager)
    {
        Model = new AuthModel();

        _windowManager = windowManager;

        CloseCommand = new RelayCommand(() => windowManager.Close(this));
    }
}