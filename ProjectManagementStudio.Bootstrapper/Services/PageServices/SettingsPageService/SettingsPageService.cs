using ProjectManagementStudio.ViewModel.PageServices.ISettingsPageService;
using ProjectManagementStudio.ViewModel.LocalizationService;
using System.Windows;

namespace ProjectManagementStudio.Bootstrapper.Services.PageServices.SettingsPageService;

internal class SettingsPageService : ISettingsPageService, ISettingsPageServiceInitializer
{
    private bool _initialized;
    private ILocalizationService _localizationService;

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
    
    public SettingsPageService(ILocalizationService localizationService)
    {
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

    public void ApplySettings()
    {
        _localizationService.SaveLanguage();

        MessageBox.Show("Настройки успешно сохранены!", "Уведомление");
    }
}
