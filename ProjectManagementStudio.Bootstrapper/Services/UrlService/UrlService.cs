using ProjectManagementStudio.Model.PathService;
using ProjectManagementStudio.ViewModel.UrlService;

namespace ProjectManagementStudio.Bootstrapper.Services.UrlService;

public class UrlService : IUrlServiceInitializer, IUrlService
{
    //словарь со всеми названиями методов и кусочками url в соответствии к ним
    private readonly Dictionary<string, string> _urlMap = new Dictionary<string, string>()
    {
        { "IsUserExists", "is-user-exists" },
        { "AddUser", "add-user" },
    };

    private bool _initialized = false;
    private readonly string _urlBase = @"https://extremedr2.eu.pythonanywhere.com";
    private readonly string _urlSecretCode = "3i7r4ybfwbatro387";
    private string _urlCommand = "";

    public string Url
    {
        get
        {
            EnsureInitialized();

            if (_urlCommand is not "")
            {
                return $"{_urlBase}/{_urlCommand}/{_urlSecretCode}"; ;
            }
            else
            {
                throw new ArgumentNullException($"Null: {nameof(_urlCommand)}");
            }
        }
    }

    public void SetUrlCommand(string command)
    {
        EnsureInitialized();

        if (_urlMap.TryGetValue(command, out string? value))
        {
            _urlCommand = _urlMap[command];
        }
        else
        {
            throw new ArgumentException("The list does not contain such commands");
        }
    }

    public void Initialize()
    {
        if (_initialized)
            throw new InvalidOperationException($"{nameof(IPathService)} is already initialized");

        _initialized = true;
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"{nameof(IPathService)} is not initialized");
    }
}