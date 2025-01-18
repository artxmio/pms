using ProjectManagementStudio.ViewModel.SettingSizeService;

namespace ProjectManagementStudio.ViewModel.PageServices.ISettingsPageService;

public interface ISettingsPageService
{
    ISettingSizeService SettingSizeService { get; set; }
}
