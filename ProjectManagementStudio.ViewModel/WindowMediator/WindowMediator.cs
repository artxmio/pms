using ProjectManagementStudio.ViewModel.MainWindow;
using ProjectManagementStudio.ViewModel.MenuWindow;
using ProjectManagementStudio.ViewModel.Windows;
using System.Windows;

namespace ProjectManagementStudio.ViewModel.WindowMediator;

public class WindowMediator : IWindowMediator
{
    private readonly IWindowManager _windowManager;
    private IMainWindowViewModel _mainWindowViewModel;
    private IMenuWindowViewModel _menuWindowViewModel;

    public WindowMediator(
        IWindowManager windowManager, 
        IMainWindowViewModel mainWindowViewModel, 
        IMenuWindowViewModel menuWindowViewModel)
    {
        _windowManager = windowManager;
        _mainWindowViewModel = mainWindowViewModel;
        _menuWindowViewModel = menuWindowViewModel;
    }

    public void Notify(IWindowViewModel viewModel)
    {
        if(viewModel is IMainWindowViewModel mainWindowViewModel)
        {
            _windowManager.Close(mainWindowViewModel); 
            _windowManager.Show(_menuWindowViewModel, false);
        }

        if (viewModel is IMainWindowViewModel menuWindowViewModel)
        {
            _windowManager.Close(menuWindowViewModel);
            _windowManager.Show(_mainWindowViewModel, false);
        }

        throw new InvalidOperationException("Unknown ViewModel type");
    }
}