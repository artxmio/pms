using ProjectManagementStudio.ViewModel.LocalizationService;
using ProjectManagementStudio.ViewModel.ThemeService;

namespace ProjectManagementStudio.ViewModel.PageServices.ISettingsPageService;

public interface ISettingsPageService
{
    ILocalizationService LocalizationService { get; set; }
    IThemeService ThemeService { get; set; }
}
