using ProjectManagementStudio.ViewModel.LocalizationService;

namespace ProjectManagementStudio.ViewModel.PageServices.ISettingsPageService;

public interface ISettingsPageService
{
    ILocalizationService LocalizationService { get; set; }
}
