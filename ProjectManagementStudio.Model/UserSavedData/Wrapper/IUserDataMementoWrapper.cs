namespace ProjectManagementStudio.Model.UserSavedData.Wrapper;

public interface IUserDataMementoWrapper
{
    string UserLogin { get; set; }
    string UserPassword { get; set; }
    bool IsRememberMe { get; set; }
    string AboutText { get; set; }

    void SaveUserData();
    void DeleteUserData();
}