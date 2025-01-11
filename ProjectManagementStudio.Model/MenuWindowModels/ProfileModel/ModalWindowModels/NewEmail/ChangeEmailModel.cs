namespace ProjectManagementStudio.Model.MenuWindowModels.ProfileModel.ModalWindowModels.NewEmail;

public class ChangeEmailModel : IChangeEmailModel
{
    private string _newEmail = "";
    private string _password = "";

    public string NewEmail
    {
        get => _newEmail; 
        set => _newEmail = value;
    }
    public string Password
    {
        get => _password; 
        set => _password = value;
    }
}
