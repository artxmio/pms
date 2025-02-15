using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.WindowModels.AuthModel;
using ProjectManagementStudio.Model.WindowModels.RegisterModel;

namespace ProjectManagementStudio.ViewModel.APIClient;

public interface IAPIClient
{
    Task<bool> IsUserExists(string login, string password);
    Task<bool> AddUser(string login, string password, string email);

    Task<ICurrentUserModel> GetUserByLogin(string login, string password);
    Task ChangeUserParametr(ICurrentUserModel currentUser, ChangeableParams parametr, string newValue);
}