using Newtonsoft.Json;
using ProjectManagementStudio.Model.PathService;
using ProjectManagementStudio.ViewModel.UrlService;
using System.IO;

namespace ProjectManagementStudio.Bootstrapper.Services.UrlService;

public class UrlService : IUrlServiceInitializer, IUrlService
{
    //словарь со всеми названиями методов и кусочками url в соответствии к ним
    private readonly Dictionary<string, string> _urlMap = new()
    {
        { "IsUserExists", "is-user-exists" },
        { "AddUser", "add-user" },
    };

    private bool _initialized = false;
    private string _urlBase = "";
    private string _token = "";
    private string _urlCommand = "";

    public string URLBase
    {
        get
        {
            EnsureInitialized();

            return $"{_urlBase}";
        }

        set
        {
            EnsureInitialized();

            _urlBase = value;
        }
    }

    public string Token
    {
        get
        {
            EnsureInitialized();

            return _token;
        }
        set
        {
            EnsureInitialized();

            _token = value;
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

        var serializeObject = File.ReadAllText("http.config.json");

        var deserializedObject = (JsonConvert.DeserializeObject<HttpConfig>(serializeObject)
            ?? throw new InvalidOperationException("Deserialized response can't be null"));

        URLBase = deserializedObject.URLBase;
        Token = deserializedObject.Token;
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"{nameof(IUrlService)} is not initialized");
    }
}