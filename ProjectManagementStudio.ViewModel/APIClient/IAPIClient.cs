using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.Model.WindowModels.AuthModel;
using ProjectManagementStudio.Model.WindowModels.RegisterModel;
using System.Collections.ObjectModel;

namespace ProjectManagementStudio.ViewModel.APIClient;

public interface IAPIClient
{
    Task<bool> IsUserExists(string login, string password);
    Task<bool> AddUser(string login, string password, string email);

    Task<ICurrentUserModel> GetUserByLogin(string login, string password);
    Task ChangeUserParametr(long id, ChangeableParams parametr, string newValue);

    Task<ObservableCollection<Project>> GetProjects(int userId);
}