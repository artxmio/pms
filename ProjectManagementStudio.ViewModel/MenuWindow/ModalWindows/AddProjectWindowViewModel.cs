using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Windows;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows;

public class AddProjectWindowViewModel : IAddProjectWindowViewModel
{
    private readonly IWindowManager _windowManager;

    public ICommand CloseWindowCommand { get; }

    public AddProjectWindowViewModel(IWindowManager windowManager)
    {
        this._windowManager = windowManager;
        CloseWindowCommand = new RelayCommand(o => _windowManager.Close(this));
    }
}
