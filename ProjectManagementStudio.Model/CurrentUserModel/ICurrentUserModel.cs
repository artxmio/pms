namespace ProjectManagementStudio.Model.CurrentUserModel;

public interface ICurrentUserModel
{
    long UserId { get; set; }
    string Login { get; set; }
    string Password { get; set; }
    string Email { get; set; }
}