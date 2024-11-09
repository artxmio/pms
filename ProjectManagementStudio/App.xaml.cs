using ProjectManagementStudio.Bootstrapper;
using ProjectManagementStudio.ViewModel.MainWindow;
using System.Windows;

namespace ProjectManagmentStudio;

public partial class App
{
    private Bootstrapper? _bootstrapper;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _bootstrapper = new Bootstrapper();

        var window = _bootstrapper.Run();
    }
}