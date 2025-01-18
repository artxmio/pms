using ProjectManagementStudio.Model.SettingSize;
using ProjectManagementStudio.Model.WindowSavedData.Wrapper;
using ProjectManagementStudio.ViewModel.SettingSizeService;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ProjectManagementStudio.Bootstrapper.Services.Settings.SettingSizeService;

internal class SettingSizeService : ISettingSizeService, ISettingSizeInitialize, INotifyPropertyChanged
{
    private IWindowSizes _selectedWindowSize;
    private ObservableCollection<IWindowSizes> _sizes;
    private readonly IWindowDataMementoWrapper _windowDataMementoWrapper;

    public IWindowSizes SelectedWindowSize
    {
        get
        {
            return _selectedWindowSize;
        }
        set
        {
            _selectedWindowSize = value;
            OnPropertyChanged();
            UpdateWindowSize(SelectedWindowSize);
        }
    }

    public int Width
    {
        get
        {
            return _windowDataMementoWrapper.Width;
        }
        set
        {
            _windowDataMementoWrapper.Width = value;
        }
    }

    public int Height
    {
        get
        {
            return _windowDataMementoWrapper.Height;
        }
        set
        {
            _windowDataMementoWrapper.Height = value;
        }
    }

    public ObservableCollection<IWindowSizes> Sizes
    {
        get
        {
            EnsureInitialized();
            return _sizes;
        }
        set
        {
            EnsureInitialized();
            _sizes = value;
        }
    }

    private bool _initialized;

    public SettingSizeService(IWindowDataMementoWrapper windowDataMementoWrapper)
    {
        _selectedWindowSize = new WindowSizes(0,0);
        _sizes = [];

        _windowDataMementoWrapper = windowDataMementoWrapper;
    }

    public void Initialize()
    {
        if (_initialized)
            throw new InvalidOperationException($"{nameof(ISettingSizeService)} is already initialized");

        _initialized = true;

        _sizes =
        [
            new WindowSizes(1056,600),
            new WindowSizes(1280, 1024),
            new WindowSizes(1920, 1080)
        ];

        LoadLastSelectedItem(); 
    }

    private void LoadLastSelectedItem()
    {
        IWindowSizes? selectedSize = _sizes.FirstOrDefault(p => p.Width == Width && p.Height == Height);

        if (selectedSize is not null)
        {
            _selectedWindowSize = selectedSize;
        }
    }

    public void UpdateWindowSize(IWindowSizes newSize)
    {
        var currentWindow = Application.Current.MainWindow;

        if (currentWindow is not null)
        {
            currentWindow.Width = newSize.Width;
            currentWindow.Height = newSize.Height;
        }
    }

    public void ApplySettings()
    {
        _windowDataMementoWrapper.SaveWindowData();
        MessageBox.Show("Настройки успешно сохранены!"); 
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"{nameof(ISettingSizeService)} is not initialized");
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
