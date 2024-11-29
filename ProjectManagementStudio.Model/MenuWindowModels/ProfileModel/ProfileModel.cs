using System.Security.Policy;
using System.Windows.Media.Imaging;

namespace ProjectManagementStudio.Model.MenuWindowModels.ProfileModel;

public class ProfileModel : IProfileModel
{
    private BitmapImage _image = new();

    public BitmapImage AvatarImage
    {
        get
        {
            return _image;
        }
        set
        {
            if (_image != value)
            {
                _image = value;
            }
        }
    }
}

