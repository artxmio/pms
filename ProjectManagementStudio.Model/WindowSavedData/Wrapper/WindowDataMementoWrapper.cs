using Newtonsoft.Json;
using ProjectManagementStudio.Model.PathService;
using ProjectManagementStudio.Model.WindowSavedData.Memento;
using System.IO;
using System.Windows;

namespace ProjectManagementStudio.Model.WindowSavedData.Wrapper;

internal class WindowDataMementoWrapper :
    IWindowDataMementoWrapper,
    IWindowDataMementoWrapperInitializer
{
    private WindowDataMemento _windowDataMemento;
    private readonly IPathService _pathService;
    private bool _initialized;
    private string _windowDataFilePath = "";

    public int Width
    {
        get
        {
            EnsureInitialized();
            return _windowDataMemento.Width;
        }
        set
        {
            EnsureInitialized();
            _windowDataMemento.Width = value;
        }
    }
    public int Height
    {
        get
        {
            EnsureInitialized();
            return _windowDataMemento.Height;
        }
        set
        {
            EnsureInitialized();
            _windowDataMemento.Height = value;
        }
    }

    public WindowDataMementoWrapper(IPathService pathService)
    {
        _pathService = pathService;
        _windowDataMemento = new WindowDataMemento();
    }

    public void Initialize()
    {
        if (_initialized)
        {
            throw new InvalidOperationException($"{nameof(IWindowDataMementoWrapper)} is already initialized");
        }

        _initialized = true;

        var userDataFolderName = "user";

        var userDataPath = Path.Combine(_pathService.ApplicationFolder, userDataFolderName);
        _windowDataFilePath = Path.Combine(userDataPath, "windowData.json");

        Directory.CreateDirectory(userDataPath);

        if (!File.Exists(_windowDataFilePath))
        {
            File.Create(_windowDataFilePath);
            return;
        }

        string jsonString = File.ReadAllText(_windowDataFilePath);

        if (string.IsNullOrEmpty(jsonString))
        {
            return;
        }

        _windowDataMemento = JsonConvert.DeserializeObject<WindowDataMemento>(jsonString)
            ?? throw new InvalidOperationException("Deserialized memento can't be null");
    }

    public void SaveWindowData()
    {
        try
        {
            EnsureInitialized();

            var json = JsonConvert.SerializeObject(_windowDataMemento)
                ?? throw new InvalidOperationException("Deserialized memento can't be null");

            File.WriteAllText(_windowDataFilePath, json);
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

    private void EnsureInitialized()
    {
        if (!_initialized)
        {
            throw new InvalidOperationException($"{nameof(IWindowDataMementoWrapper)} is not initialized");
        }
    }
}
