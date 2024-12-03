using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProjectManagementStudio.Bootstrapper.Services.AvatarService;

public interface IAvatarService
{
    string AvatarFilePath { get; set; }

    BitmapImage AvatarImage { get; set; }

    void ChangeAvatar(string newImagePath);
}