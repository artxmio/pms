namespace ProjectManagementStudio.Model.MenuWindowModels.ProfileModel.ModalWindowModels.NewPassword;

public class ChangePasswordModel : IChangePasswordModel
{
    private string _oldPassword = "";
    private string _newPassword = "";

    public string OldPassword
    {
        get => _oldPassword;
        set => _oldPassword = value;
    }
    public string NewPassword
    {
        get => _newPassword; 
        set => _newPassword = value;
    }
}
