using Autofac;
using ProjectManagementStudio.View.MainWindow;
using ProjectManagementStudio.View.MenuWindow;
using ProjectManagementStudio.View.MenuWindow.ModalWindows;
using ProjectManagementStudio.View.WindowFactory;
using ProjectManagementStudio.ViewModel.MainWindow;
using ProjectManagementStudio.ViewModel.MenuWindow;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.ChangeEmail;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.ChangeLogin;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.ChangePassword;
using ProjectManagementStudio.ViewModel.Windows;

namespace ProjectManagementStudio.Bootstrapper.Factories;

internal class WindowFactory : IWindowFactory
{
    private readonly IComponentContext _componentContext;

    private readonly Dictionary<Type, Type> _typeMap = new()
        {
            { typeof(IMainWindowViewModel), typeof(IMainWindow) },
            { typeof(IMenuWindowViewModel), typeof(IMenuWindow) },
            { typeof(ILoginChangeDialogViewModel), typeof(ILoginChangeDialog) },
            { typeof(IPasswordChangeDialogViewModel), typeof(IPasswordChangeDialog) },
            { typeof(IEmailChangeDialogViewModel), typeof(IEmailChangeDialog) }
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