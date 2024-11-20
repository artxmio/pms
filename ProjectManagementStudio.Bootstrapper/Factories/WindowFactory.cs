using Autofac;
using ProjectManagementStudio.View.MainWindow;
using ProjectManagementStudio.View.WindowFactory;
using ProjectManagementStudio.ViewModel.MainWindow;
using ProjectManagementStudio.ViewModel.Windows;

namespace ProjectManagementStudio.Bootstrapper.Factories;

/*
    Фабрика отвечает за создание окон исходя из их ViewModel
*/

internal class WindowFactory : IWindowFactory
{
    private readonly IComponentContext _componentContext;

    private readonly Dictionary<Type, Type> _typeMap = new()
        {
            { typeof(IMainWindowViewModel), typeof(IMainWindow) }
        };

    public WindowFactory(IComponentContext componentContext)
    {
        _componentContext = componentContext;
    }

    public IWindow Create<T>(T viewModel)
        where T : IWindowViewModel
    {
        if (!_typeMap.TryGetValue(typeof(T), out var windowType))
        {
            throw new InvalidOperationException($"There is no window registered for {typeof(T)}");
        }

        var instanse = _componentContext.Resolve(windowType, TypedParameter.From(viewModel));

        return (IWindow)instanse;
    }
}