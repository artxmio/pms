using Microsoft.Win32;
using ProjectManagementStudio.Model.PathService;
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
            throw new InvalidOperationException($"{nameof(IAvatarService)} is already initialized");

        _initialized = true;

        var avatarFolderName = Path.Combine(_pathService.ApplicationFolder, "images");

        if (!Directory.Exists(avatarFolderName))
        {
            Directory.CreateDirectory(avatarFolderName);
        }

        AvatarFilePath = Path.Combine(avatarFolderName.ToString(), "avatar.jpg");

        if (!File.Exists(AvatarFilePath.ToString()))
        {
            File.Copy("images\\user.png", AvatarFilePath);
        }
    }

    public BitmapImage ChangeAvatar(string newImagePath)
    {
        try
        {
            AvatarFilePath = newImagePath;
            File.Copy(newImagePath, AvatarFilePath, true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        return new BitmapImage(new Uri(AvatarFilePath));
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