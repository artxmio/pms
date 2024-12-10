using ProjectManagementStudio.Model.CurrentUserModel;

namespace ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;

public interface ICurrentUserService
{
    ICurrentUserModel CurrentUser { get; set; }
}