using ProjectManagementStudio.Model.WindowModels.AuthModel;
using ProjectManagementStudio.Model.WindowModels.RegisterModel;

namespace ProjectManagementStudio.ViewModel.APIClient;

public interface IAPIClient
{
    public Task<bool> IsUserExists(AuthModel user);
    public Task AddUser(RegisterModel user);
}