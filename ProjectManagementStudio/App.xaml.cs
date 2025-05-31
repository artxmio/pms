using ProjectManagementStudio.Bootstrapper;
using System.Windows;

namespace ProjectManagmentStudio;

public partial class App
{
    private Bootstrapper? _bootstrapper;

    public App()
    {

    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
     
        _bootstrapper = new Bootstrapper();

        var window = _bootstrapper.Run();
    }
}