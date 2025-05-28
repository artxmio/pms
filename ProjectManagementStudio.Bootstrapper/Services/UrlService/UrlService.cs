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
        { "IsUserExists", "api-v2/user/exists" },
        { "AddUser", "api-v2/user/create" },
        { "ChangeUserParametr", "api-v2/user/update" },
        { "GetProjects", "api-v2/project/get-projects-for-user" },
        { "AddProject", "api-v2/project/create" }
    };

    private bool _initialized = false;

    private string _urlBase = "";
    private string _token = "";
    private string _urlEndpoint = "";

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

    public string URLEndpoint
    {
        get
        {
            EnsureInitialized();

            return _urlEndpoint;
        }
        set
        {
            EnsureInitialized();

            if (_urlMap.TryGetValue(value, out string? s))
            {
                _urlEndpoint = _urlMap[value];
            }
            else
            {
                throw new ArgumentException("The list does not contain such commands");
            }
        }
    }

    public void Initialize()
    {
        if (_initialized)
            throw new InvalidOperationException($"{nameof(IPathService)} is already initialized");

        _initialized = true;

        var serializeObject = File.ReadAllText("http.config.json");

        var deserializedObject = JsonConvert.DeserializeObject<HttpConfig>(serializeObject)
            ?? throw new InvalidOperationException("Deserialized response can't be null");

        URLBase = deserializedObject.URLBase;
        Token = deserializedObject.Token;
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"{nameof(IUrlService)} is not initialized");
    }
}