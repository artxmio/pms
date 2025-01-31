using ProjectManagementStudio.ViewModel.LocalizationService;
using ProjectManagementStudio.ViewModel.SettingSizeService;

namespace ProjectManagementStudio.ViewModel.PageServices.ISettingsPageService;

public interface ISettingsPageService
{
    ISettingSizeService SettingSizeService { get; set; }
    ILocalizationService LocalizationService { get; set; }
}
