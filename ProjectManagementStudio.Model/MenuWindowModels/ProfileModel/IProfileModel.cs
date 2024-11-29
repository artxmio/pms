using System.Windows.Media.Imaging;

namespace ProjectManagementStudio.Model.MenuWindowModels.ProfileModel;

public interface IProfileModel
{
    BitmapImage AvatarImage { get; set; }
}