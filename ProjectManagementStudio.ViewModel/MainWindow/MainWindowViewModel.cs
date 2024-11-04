using ProjectManagementStudio.Model.TestModel;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Windows;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.MainWindow;

public class MainWindowViewModel : IMainWindowViewModel
{
    private readonly IWindowManager _windowManager;

    private readonly ICommand _closeCommand;

    public TestModel test { get; set; }

    public ICommand Close => _closeCommand;

    public MainWindowViewModel(IWindowManager windowManager)
    {
        test = new();

        _windowManager = windowManager;

        _closeCommand = new RelayCommand(() => _windowManager.Close(this));
    }
}