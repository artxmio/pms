namespace ProjectManagementStudio.Model.UserSavedData.Memento;

public interface IUserDataMemento
{
    string UserLogin { get; set; }
    string UserPassword { get; set; }
    bool IsRememberMe { get; set; }
    string AboutText { get; set; }
}