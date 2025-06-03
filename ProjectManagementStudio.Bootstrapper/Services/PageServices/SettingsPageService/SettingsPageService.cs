using ProjectManagementStudio.ViewModel.PageServices.ISettingsPageService;
using ProjectManagementStudio.ViewModel.LocalizationService;
using System.Windows;
using ProjectManagementStudio.ViewModel.ThemeService;

namespace ProjectManagementStudio.Bootstrapper.Services.PageServices.SettingsPageService;

internal class SettingsPageService : ISettingsPageService, ISettingsPageServiceInitializer
{
    private bool _initialized;
    private ILocalizationService _localizationService;

    public ILocalizationService LocalizationService
    {
        get => _localizationService;
        set => _localizationService = value;
    }

    private IThemeService _themeService;

    public IThemeService ThemeService
    {
        get => _themeService;
        set => _themeService = value;
    }

    public SettingsPageService(ILocalizationService localizationService, IThemeService themeService)
    {
        _localizationService = localizationService;
        _themeService = themeService;
    }

    public void Initialize()
    {
        if (_initialized)
        {
            throw new InvalidOperationException($"{nameof(ISettingsPageService)} is already initialized");
        }

        _initialized = true;
    }
}
