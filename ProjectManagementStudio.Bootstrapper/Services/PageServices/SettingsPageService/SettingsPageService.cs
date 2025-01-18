using ProjectManagementStudio.ViewModel.PageServices.ISettingsPageService;
using ProjectManagementStudio.ViewModel.SettingSizeService;

namespace ProjectManagementStudio.Bootstrapper.Services.PageServices.SettingsPageService;

internal class SettingsPageService : ISettingsPageService, ISettingsPageServiceInitializer
{
    private bool _initialized;
    private ISettingSizeService _settingsPageService;

    public ISettingSizeService SettingSizeService
    {
        get
        {
            return _settingsPageService;
        }

        set
        {
            _settingsPageService = value;
        }
    }

    public SettingsPageService(ISettingSizeService settingSizeService)
    {
        _settingsPageService = settingSizeService;
    }

    public void Initialize()
    {
        if (_initialized)
        {
            throw new InvalidOperationException($"{nameof(ISettingsPageService)} is already initialized");
        }

        _initialized = true;
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"{nameof(ISettingsPageService)} is not initialized");
    }
}
