using ProjectManagementStudio.Model.TestModel;
using ProjectManagementStudio.ViewModel.Windows;
using ProjectManagementStudio.ViewModel.Command;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.MainWindow;

public class MainWindowViewModel : IMainWindowViewModel
{
    private readonly IWindowManager _windowManager;

    private readonly ICommand _closeCommand;

    public TestModel Test { get; set; }

    public ICommand Close => _closeCommand;

    public MainWindowViewModel(IWindowManager windowManager)
    {
        Test = new TestModel();

        _windowManager = windowManager;

        _closeCommand = new RelayCommand(() => _windowManager.Close(this));
    }
}