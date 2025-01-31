using ProjectManagementStudio.View.WindowFactory;
using ProjectManagementStudio.ViewModel.Windows;
using System.Windows;

namespace ProjectManagementStudio.View.WindowManager;

public class WindowsManager : IWindowManager
{
    private readonly Dictionary<IWindowViewModel, IWindow> _viewModelToWindowMap = new();

    private readonly IWindowFactory _windowFactory;

    public WindowsManager(IWindowFactory windowFactory)
    {
        _windowFactory = windowFactory;
    }

    public IWindow Show<T>(T viewModel, bool isDialog = false)
        where T : IWindowViewModel
    {
        var newWindow = _windowFactory.Create(viewModel);

        if (!_viewModelToWindowMap.ContainsKey(viewModel))
            _viewModelToWindowMap.Add(viewModel, newWindow);

        if (newWindow is not Window window)
        {
            throw new NotImplementedException();
        }

        if (isDialog)
        {
            window.DataContext = viewModel;
            window.ShowDialog();
        }
        else
        {
            window.Show();
        }

        return newWindow;
    }

    public void Close<T>(T viewModel)
        where T : IWindowViewModel
    {
        if (_viewModelToWindowMap.TryGetValue(viewModel, out var window))
        {
            window.Close();
            _viewModelToWindowMap.Remove(viewModel);
        }
    }

    public void RollWindow<T>(T viewModel)
        where T : IWindowViewModel
    {
        if (_viewModelToWindowMap.TryGetValue(viewModel, out var window))
        {
            var w = window as Window;
            w.WindowState = WindowState.Minimized;
        }
    }

    public void RestoreWindow<T>(T viewModel)
        where T : IWindowViewModel
    {
        if (_viewModelToWindowMap.TryGetValue(viewModel, out var window))
        {
            var w = window as Window ?? throw new NullReferenceException();

            if (w.WindowState != WindowState.Maximized)
            {
                w.WindowState = WindowState.Maximized;
            }
            else
            {
                w.WindowState = WindowState.Normal;
            }
        }
    }
}