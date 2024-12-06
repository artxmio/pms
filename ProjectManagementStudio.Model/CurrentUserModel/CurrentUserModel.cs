using ProjectManagementStudio.Model.WindowModels.AuthModel;

namespace ProjectManagementStudio.Model.CurrentUserModel;

public class CurrentUserModel : ICurrentUserModel
{
    public long UserId
    {
        get;
        set;
    }
    public string Login
    {
        get;
        set;
    }
    public string Password
    {
        get;
        set;
    }
    public string Email
    {
        get;
        set;
    }

    public CurrentUserModel()
    {
        UserId = -1;
        Login = string.Empty;
        Password = string.Empty;
        Email = string.Empty;
    }
}
