namespace ProjectManagementStudio.Model.AuthModel;

public interface IAuthModel
{
    string Login { get; set; }
    string Password { get; set; }
    string Email { get; set; }
}