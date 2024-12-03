using Microsoft.Win32;
using ProjectManagementStudio.Model.PathService;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProjectManagementStudio.Bootstrapper.Services.AvatarService;

internal class AvatarService : IAvatarService, IAvatarServiceInitializer
{
    private string _avatarFilePath = "";
    private BitmapImage _avatarImage;

    private bool _initialized;
    private readonly IPathService _pathService;

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
        }
    }

    public BitmapImage AvatarImage
    {
        get
        {
            return _avatarImage;
        }

        set
        {
            _avatarImage = value;
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

        _avatarFilePath = Path.Combine(avatarFolderName.ToString(), "avatar.jpg");

        if (!File.Exists(_avatarFilePath.ToString()))
        {
            File.Copy("images\\user.png", _avatarFilePath, overwrite: true);
        }

        AvatarImage = new BitmapImage(new Uri(AvatarFilePath));
    }

    public void ChangeAvatar(string newImagePath)
    {
        try
        {
            File.Copy(newImagePath, $"{AvatarFilePath}", true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"{nameof(IAvatarService)} is not initialized");
    }
}