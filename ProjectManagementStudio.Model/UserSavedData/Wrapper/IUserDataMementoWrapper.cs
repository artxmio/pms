namespace ProjectManagementStudio.Model.UserSavedData.Wrapper;

public interface IUserDataMementoWrapper
{
    string UserLogin { get; set; }
    string UserPassword { get; set; }
    string Email { get; set; }
    bool IsRememberMe { get; set; }
    string AboutText { get; set; }
    bool IsFileNull { get; set; }

    void SaveUserData();
    void DeleteUserData();
}