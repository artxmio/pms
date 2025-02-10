using ProjectManagementStudio.Model.WindowModels.BaseModel;

namespace ProjectManagementStudio.Model.WindowModels.AuthModel;

public interface IAuthModel
{
    string Login { get; set; }
    string Password { get; set; }
    bool IsValid { get; set; }

    public bool IsRememberMe { get; set; }
    void Validate();
}