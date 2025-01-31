using ProjectManagementStudio.ViewModel.PageServices.ISettingsPageService;
using ProjectManagementStudio.ViewModel.SettingSizeService;
using ProjectManagementStudio.ViewModel.LocalizationService;

namespace ProjectManagementStudio.Bootstrapper.Services.PageServices.SettingsPageService;

internal class SettingsPageService : ISettingsPageService, ISettingsPageServiceInitializer
{
    private bool _initialized;
    private ISettingSizeService _settingsSizeService;
    private ILocalizationService _localizationService;

    public ISettingSizeService SettingSizeService
    {
        get
        {
            return _settingsSizeService;
        }

        set
        {
            _settingsSizeService = value;
        }
    }

    public ILocalizationService LocalizationService
    {
        get
        {
            return _localizationService;
        }

        set
        {
            _localizationService = value;
        }
    }
    
    public SettingsPageService(ISettingSizeService settingSizeService, ILocalizationService localizationService)
    {
        _settingsSizeService = settingSizeService;
        _localizationService = localizationService;
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
