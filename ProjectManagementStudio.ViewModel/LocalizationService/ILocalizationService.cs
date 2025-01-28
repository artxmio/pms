using System.Globalization;

namespace ProjectManagementStudio.ViewModel.LocalizationService;

public interface ILocalizationService
{
    List<CultureInfo> Languages { get; }
    CultureInfo Language { get; set; }
    event EventHandler LanguageChanged;
}