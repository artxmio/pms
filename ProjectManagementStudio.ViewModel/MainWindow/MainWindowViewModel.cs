using ProjectManagementStudio.Model.TestModel;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Windows;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.MainWindow;

public class MainWindowViewModel : IMainWindowViewModel
{
    private readonly IWindowManager _windowManager;

    public TestModel test { get; set; }

    private readonly IWindowManager _windowManager;

    public ICommand CloseCommand { get; set; }

    public MainWindowViewModel(IWindowManager windowManager)
    {
        test = new();

        _windowManager = windowManager;

        CloseCommand = new RelayCommand(() => windowManager.Close(this));
    }
}