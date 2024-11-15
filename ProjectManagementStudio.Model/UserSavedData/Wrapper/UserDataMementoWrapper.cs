using ProjectManagementStudio.Model.UserSavedData.Memento;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace ProjectManagementStudio.Model.UserSavedData.Wrapper;

internal class UserDataMementoWrapper :
    IUserDataMementoWrapper,
    IUserDataMementoWrapperInitializer,
    IDisposable
{
    private UserDataMemento _userDataMemento;
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

    public UserDataMementoWrapper()
    {
        _userDataMemento = new(); 
    }

    public void Initialize()
    {
        if (_initialized)
        {
            throw new InvalidOperationException($"{nameof(IUserDataMementoWrapper)} is already initialized");
        }

        _initialized = true;

        var localApplicationPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var companyName = "artxm";
        var applicationName = "pms";
        var userDataFolderName = "user";

        var userDataPath = Path.Combine(localApplicationPath, companyName, applicationName, userDataFolderName);

        _userDataFilePath = Path.Combine(userDataPath, "userData.json");

        Directory.CreateDirectory(userDataPath);

        if (!File.Exists(_userDataFilePath))
        {
            return;
        }

        string jsonString = File.ReadAllText(_userDataFilePath);

        _userDataMemento = JsonConvert.DeserializeObject<UserDataMemento>(jsonString);
    }

    public void Dispose()
    {
        try
        {
            EnsureInitialized();

            var json = JsonConvert.SerializeObject(_userDataMemento);

            File.WriteAllText(_userDataFilePath, json);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
        {
            throw new InvalidOperationException($"{nameof(IUserDataMementoWrapper)} is not initialized");
        }
    }
}
