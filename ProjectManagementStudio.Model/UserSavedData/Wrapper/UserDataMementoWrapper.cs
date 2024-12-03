using ProjectManagementStudio.Model.UserSavedData.Memento;
using System.IO;
using Newtonsoft.Json;
using ProjectManagementStudio.Model.PathService;
using System.Windows.Media.Imaging;

namespace ProjectManagementStudio.Model.UserSavedData.Wrapper;

internal class UserDataMementoWrapper :
    IUserDataMementoWrapper,
    IUserDataMementoWrapperInitializer
{
    private UserDataMemento _userDataMemento;
    private readonly IPathService _pathService;
    private bool _initialized = false;
    private string _userDataFilePath = "";

    public string UserLogin
    {
        get
        {
            EnsureInitialized();
            return _userDataMemento.UserLogin;
        }

        set
        {
            EnsureInitialized();
            _userDataMemento.UserLogin = value;
        }
    }
    public string UserPassword
    {
        get
        {
            EnsureInitialized();
            return _userDataMemento.UserPassword;
        }

        set
        {
            EnsureInitialized();
            _userDataMemento.UserPassword = value;
        }
    }

    private bool _isRememberMe;

    public bool IsRememberMe
    {
        get
        {
            EnsureInitialized();
            return _isRememberMe;
        }
        set
        {
            EnsureInitialized();
            _isRememberMe = value;

            if (_isRememberMe)
            {
                SaveUserData();
            }
            else
            {
                DeleteUserData();
            }
        }
    }

    public UserDataMementoWrapper(IPathService pathService)
    {
        _pathService = pathService;

        _userDataMemento = new();
    }

    public void Initialize()
    {
        if (_initialized)
        {
            throw new InvalidOperationException($"{nameof(IUserDataMementoWrapper)} is already initialized");
        }

        _initialized = true;

        var userDataFolderName = "user";

        var userDataPath = Path.Combine(_pathService.ApplicationFolder, userDataFolderName);
        _userDataFilePath = Path.Combine(userDataPath, "userData.json");

        Directory.CreateDirectory(userDataPath);

        if (!File.Exists(_userDataFilePath))
        {
            return;
        }

        string jsonString = File.ReadAllText(_userDataFilePath);

        _userDataMemento = JsonConvert.DeserializeObject<UserDataMemento>(jsonString)
            ?? throw new InvalidOperationException("Deserialized memento can't be null");
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
        {
            throw new InvalidOperationException($"{nameof(IUserDataMementoWrapper)} is not initialized");
        }
    }

    private void SaveUserData()
    {
        EnsureInitialized();

        var json = JsonConvert.SerializeObject(_userDataMemento);

        File.WriteAllText(_userDataFilePath, json);
    }

    private void DeleteUserData()
    {
        EnsureInitialized();
        File.Delete(_userDataFilePath);
    }
}