using ProjectManagementStudio.Model.SettingSize;
using ProjectManagementStudio.ViewModel.SettingSizeService;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ProjectManagementStudio.Bootstrapper.Services.Settings.SettingSizeService;

internal class SettingSizeService : ISettingSizeService, ISettingSizeInitialize, INotifyPropertyChanged
{
    private ObservableCollection<IWindowSizes> _sizes;
    private IWindowSizes _current;

    public IWindowSizes Current
    {
        get
        {
            EnsureInitialized();
            return _current;
        }
        set
        {
            EnsureInitialized();
            _current = value;
            UpdateWindowSize(Current);
            OnPropertyChanged();
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

    public SettingSizeService()
    {
        _sizes = [];
        _current = new WindowSizes(0, 0);
    }

    public void Initialize()
    {
        if (_initialized)
            throw new InvalidOperationException($"{nameof(ISettingSizeService)} is already initialized");

        _initialized = true;

        _sizes =
        [
            new WindowSizes(800,600),
            new WindowSizes(1280, 1024),
            new WindowSizes(1920, 1080)
        ];

        Current = Sizes[2];
        UpdateWindowSize(Current);
    }

    private static void UpdateWindowSize(IWindowSizes newSize)
    {
        var currentWindow = Application.Current.MainWindow;

        if (currentWindow is not null)
        {
            currentWindow.Width = newSize.Width;
            currentWindow.Height = newSize.Height;
        }
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
