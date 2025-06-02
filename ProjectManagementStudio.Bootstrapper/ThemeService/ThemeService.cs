using ProjectManagementStudio.Model.Enums;
using ProjectManagementStudio.ViewModel.ThemeService;
using System.Windows;

namespace ProjectManagementStudio.Bootstrapper.ThemeService;

internal class ThemeService : IThemeService, IThemeServiceInitializer
{
    private bool _isInitialized;
    private Theme _current = Theme.Unknown;

    public Theme Current
    {
        get => _current;
        set => _current = value;
    }

    public ThemeService()
    {
        
    }

    public void SetTheme(Theme theme)
    {
        if (theme == Theme.Unknown)
        {
            throw new ArgumentNullException(nameof(theme));
        }

        var styles = new ResourceDictionary()
        {
            Source = GetThemeUri(theme)
        };

        ResourceDictionary? dictionaryToRemove = Application.Current.Resources.MergedDictionaries
                                            .FirstOrDefault(d => (bool)d.Source?.OriginalString.Contains("Theme.xaml"));

        if (dictionaryToRemove != null)
        {
            Application.Current.Resources.MergedDictionaries.Remove(dictionaryToRemove);
        }

        Application.Current.Resources.MergedDictionaries.Add(styles);

        Current = theme;
    }

    public void Initialize()
    {
        if (_isInitialized)
        {
            throw new ApplicationException($"{nameof(ThemeService)} is already initialized");
        }

        _isInitialized = true;

        var themeIndex = Properties.Settings.Default.Theme;

        var styles = new ResourceDictionary()
        {
            Source = GetThemeUri((Theme)themeIndex)
        };

        Application.Current.Resources.MergedDictionaries.Add(styles);

        Current = (Theme)themeIndex;
    }

    private Uri GetThemeUri(Theme theme)
    {
        return theme switch
        {
            Theme.Unknown => throw new ApplicationException("Unknow theme."),
            Theme.Default => new Uri("pack://application:,,,/ProjectManagementStudio.View;component/Themes/DefaultTheme.xaml", UriKind.RelativeOrAbsolute),
            Theme.Red => throw new NotImplementedException(),
            Theme.Green => throw new NotImplementedException(),
            Theme.RedGray => throw new NotImplementedException(),
            Theme.Blue => throw new NotImplementedException(),
            Theme.Orange => throw new NotImplementedException(),
            _ => throw new ApplicationException(),
        };
    }
}
