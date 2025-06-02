using ProjectManagementStudio.Model.Enums;

namespace ProjectManagementStudio.ViewModel.ThemeService;

public interface IThemeService
{
    Theme Current { get; set; }
    void SetTheme(Theme theme);
}
