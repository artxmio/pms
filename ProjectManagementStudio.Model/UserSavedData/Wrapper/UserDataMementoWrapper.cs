using ProjectManagementStudio.Model.UserSavedData.Memento;
using System.IO;
using Newtonsoft.Json;
using ProjectManagementStudio.Model.PathService;
using System.Windows;

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

    public bool IsRememberMe
    {
        get
        {
            EnsureInitialized();
            return _userDataMemento.IsRememberMe;
        }
        set
        {
            EnsureInitialized();
            _userDataMemento.IsRememberMe = value;

            if (_userDataMemento.IsRememberMe)
            {
                SaveUserData();
            }
            else
            {
                DeleteUserData();
            }
        }
    }

    public bool IsFileNull
    {
        get;
        set;
    } = false;

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
            File.Create(_userDataFilePath);
            IsFileNull = true;
            return;
        }

        string jsonString = File.ReadAllText(_userDataFilePath);

        if (string.IsNullOrEmpty(jsonString))
        {
            IsFileNull = true;
            return;
        }

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

    public void SaveUserData()
    {
        try
        {
            EnsureInitialized();

            var json = JsonConvert.SerializeObject(_userDataMemento)
                ?? throw new InvalidOperationException("Deserialized memento can't be null");

            File.WriteAllText(_userDataFilePath, json);
        }
        catch (JsonSerializationException ex)
        {
            MessageBox.Show("Ошибка сериализации JSON: " + ex.Message, "Внимание");
        }
        catch (JsonWriterException ex)
        {
            MessageBox.Show("Ошибка записи JSON: " + ex.Message, "Внимание");
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show("Ошибка: " + ex.Message, "Внимание");
        }
        catch (IOException ex)
        {
            MessageBox.Show("Ошибка ввода-вывода при записи в файл: " + ex.Message, "Внимание");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Ошибка доступа при записи в файл: " + ex.Message, "Внимание");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Произошла неожиданная ошибка: " + ex.Message, "Внимание");
        }
    }

    public void DeleteUserData()
    {
        try
        {
            EnsureInitialized();

            _userDataMemento.IsRememberMe = false;

            var json = JsonConvert.SerializeObject(_userDataMemento);

            File.WriteAllText(_userDataFilePath, json);
        }
        catch (JsonSerializationException ex)
        {
            MessageBox.Show("Ошибка сериализации JSON: " + ex.Message, "Внимание");
        }
        catch (JsonWriterException ex)
        {
            MessageBox.Show("Ошибка записи JSON: " + ex.Message, "Внимание");
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show("Ошибка инициализации: " + ex.Message, "Внимание");
        }
        catch (IOException ex)
        {
            MessageBox.Show("Ошибка ввода-вывода при записи в файл: " + ex.Message, "Внимание");
        }
        catch (UnauthorizedAccessException ex)
        {
            MessageBox.Show("Ошибка доступа при записи в файл: " + ex.Message, "Внимание");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Произошла неожиданная ошибка: " + ex.Message, "Внимание");
        }
    }
}