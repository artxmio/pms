namespace ProjectManagementStudio.Model.AuthModel;

public interface IAuthModel
{
    string login { get; set; }
    string password { get; set; }
    string email { get; set; }
}