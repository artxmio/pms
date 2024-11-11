using ProjectManagementStudio.Model.TestModel;
using ProjectManagementStudio.ViewModel.Windows;
using ProjectManagementStudio.ViewModel.Command;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.MainWindow;

public class MainWindowViewModel : IMainWindowViewModel
{
    private readonly IWindowManager _windowManager;

    public TestModel test { get; set; }

    public ICommand CloseCommand { get; set; }

    public MainWindowViewModel(IWindowManager windowManager)
    {
        test = new TestModel();

        _windowManager = windowManager;

        CloseCommand = new RelayCommand(() => windowManager.Close(this));
    }
}