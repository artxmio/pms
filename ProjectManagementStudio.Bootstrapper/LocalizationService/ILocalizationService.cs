using System.Globalization;

namespace ProjectManagementStudio.Bootstrapper.LocalizationService;

public interface ILocalizationService
{
    static List<CultureInfo> Languages { get; }
    static CultureInfo Language { get; set; }
    static event EventHandler LanguageChanged;
}