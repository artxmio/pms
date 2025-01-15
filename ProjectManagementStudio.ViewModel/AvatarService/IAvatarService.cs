using System.Windows.Media.Imaging;

namespace ProjectManagementStudio.ViewModel.AvatarService;

public interface IAvatarService
{
    string AvatarFilePath { get; set; }

    BitmapImage ChangeAvatar(string newImagePath);
}