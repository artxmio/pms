namespace ProjectManagementStudio.Model.MenuWindowModels.ProfileModel.ModalWindowModels.NewLogin;

class ChangeLoginModel : IChangeLoginModel
{
    private string _newLogin = "";
    private string _password = "";

    public string NewLogin
    {
        get => _newLogin;

        set => _newLogin = value;
    }

    public string Password
    {
        get => _password;
        set => _password = value;
    }
}
