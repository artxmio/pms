using ProjectManagementStudio.Model.WindowModels.BaseModel;

namespace ProjectManagementStudio.Model.WindowModels.RegisterModel;

public interface IRegisterModel
{
    string Login { get; set; }
    string Password { get; set; }
    string Email { get; set; }

    void Validate();
}