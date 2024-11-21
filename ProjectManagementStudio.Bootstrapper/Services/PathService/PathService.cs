using ProjectManagementStudio.Model.PathService;
using System.IO;

namespace ProjectManagementStudio.Bootstrapper.Services.PathService;

internal class PathService : IPathServiceInitializer, IPathService
{
    private string _applicationFolder = string.Empty;
    private bool _initialized = false;

    public string ApplicationFolder
    {
        get
        {
            EnsureInitialized();

            return _applicationFolder;
        }
        private set => _applicationFolder = value;
    }

    public void Initialize()
    {
        if (_initialized)
            throw new InvalidOperationException($"{nameof(IPathService)} is already initialized");

        _initialized = true;

        var localApplicationPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var companyName = "artxm";
        var applicationName = "pms";

        ApplicationFolder = Path.Combine(localApplicationPath, companyName, applicationName);
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"{nameof(IPathService)} is not initialized");
    }
}