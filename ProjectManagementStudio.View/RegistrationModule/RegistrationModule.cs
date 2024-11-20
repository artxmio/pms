using Autofac;
using ProjectManagementStudio.View.MainWindow;
using ProjectManagementStudio.View.MenuWindow;
using ProjectManagementStudio.ViewModel.Windows;

namespace ProjectManagementStudio.View.RegistrationModule;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<MainWindow.MainWindow>().As<IMainWindow>().InstancePerDependency();
        builder.RegisterType<MenuWindow.MenuWindow>().As<IMenuWindow>().InstancePerDependency();

        builder.RegisterType<WindowManager.WindowsManager>().As<IWindowManager>().SingleInstance();
    }
}