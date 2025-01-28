using System.Globalization;
using System.Windows;

namespace ProjectManagementStudio.Bootstrapper.LocalizationService;

internal class LocalizationService : ILocalizationService
{
    private static List<CultureInfo> _languages = new List<CultureInfo>();

    public static event EventHandler LanguageChanged;

    public static List<CultureInfo> Languages
    {
        get => _languages;
    }

    public static CultureInfo Language
    {
        get
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture;
        }
        set
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == System.Threading.Thread.CurrentThread.CurrentUICulture)
            {
                return;
            }

            //1. Меняем язык приложения:
            System.Threading.Thread.CurrentThread.CurrentUICulture = value;

            //2. Создаём ResourceDictionary для новой культуры
            ResourceDictionary dict = [];

            dict.Source = value.Name switch
            {
                "ru-RU" => new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative),
                _ => new Uri("Resources/lang.xaml", UriKind.Relative),
            };

            //3. Находим старую ResourceDictionary и удаляем его и добавляем новую ResourceDictionary
            ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                          where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                                          select d).First();
            if (oldDict != null)
            {
                int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(dict);
            }

            //4. Вызываем евент для оповещения всех окон.
            LanguageChanged(Application.Current, new EventArgs());
        }
    }

    public LocalizationService()
    {
        _languages.Clear();
        _languages.Add(new CultureInfo("en-US"));
        _languages.Add(new CultureInfo("ru-RU"));
    }
}
