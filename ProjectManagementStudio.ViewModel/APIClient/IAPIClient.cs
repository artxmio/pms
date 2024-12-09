using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.WindowModels.AuthModel;
using ProjectManagementStudio.Model.WindowModels.RegisterModel;

namespace ProjectManagementStudio.ViewModel.APIClient;

public interface IAPIClient
{
    Task<bool> IsUserExists(IAuthModel user);
    Task AddUser(IRegisterModel user);

    Task<ICurrentUserModel> GetUserByLogin(IAuthModel authModel);
    Task ChangeLogin(ICurrentUserModel currentUser, string newValue);
}