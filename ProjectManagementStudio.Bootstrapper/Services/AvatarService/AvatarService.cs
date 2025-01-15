using Microsoft.Win32;
using ProjectManagementStudio.Model.PathService;
using ProjectManagementStudio.ViewModel.AvatarService;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProjectManagementStudio.Bootstrapper.Services.AvatarService;

internal class AvatarService : IAvatarService, IAvatarServiceInitializer, INotifyPropertyChanged
{
    private string _avatarFilePath = "";

    private bool _initialized;
    private readonly IPathService _pathService;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string AvatarFilePath
    {
        get
        {
            return _avatarFilePath
                ?? throw new ArgumentNullException($"{nameof(_avatarFilePath)} is null");
        }
        set
        {
            _avatarFilePath = value;
            OnPropertyChanged();
        }
    }

    public AvatarService(IPathService pathService)
    {
        _pathService = pathService;
    }

    public void Initialize()
    {
        if (_initialized)
        {
            throw new InvalidOperationException($"{nameof(IAvatarService)} is already initialized");
        }

        _initialized = true;

        var avatarFolderName = Path.Combine(_pathService.ApplicationFolder, "images");

        if (!Directory.Exists(avatarFolderName))
        {
            Directory.CreateDirectory(avatarFolderName);
        }

        AvatarFilePath = Path.Combine(avatarFolderName, "avatar.jpg");

        if (!File.Exists(AvatarFilePath))
        {
            File.Copy("images\\avatar.png", AvatarFilePath);
        }
    }

    public BitmapImage ChangeAvatar(string newImagePath)
    {
        try
        {
            var newAvatar = new BitmapImage();
            newAvatar.BeginInit();
            newAvatar.UriSource = new Uri(newImagePath);
            newAvatar.CacheOption = BitmapCacheOption.OnDemand;
            newAvatar.EndInit();

            File.Copy(newImagePath, AvatarFilePath, overwrite: true);
            return newAvatar;
        }
        catch (FileNotFoundException ex)
        {
            MessageBox.Show($"Файл не найден: {ex.Message}");
            return null;
        }
        catch (UriFormatException ex)
        {
            MessageBox.Show($"Неправильный формат URI: {ex.Message}");
            return null;
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show($"Некорректный аргумент: {ex.Message}");
            return null;
        }
        catch (IOException ex)
        {
            MessageBox.Show($"Ошибка ввода-вывода: {ex.Message}");
            return null;
        }
        catch (NotSupportedException ex)
        {
            MessageBox.Show($"Формат изображения не поддерживается: {ex.Message}");
            return null;
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show($"Неверная операция: {ex.Message}");
            return null;
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"{nameof(IAvatarService)} is not initialized");
    }
}