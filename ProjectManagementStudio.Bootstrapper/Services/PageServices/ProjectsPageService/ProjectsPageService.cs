using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.PageServices.IProjectsPageService;
using System.Collections.ObjectModel;

namespace ProjectManagementStudio.Bootstrapper.Services.PageServices.ProjectsPageService;

internal class ProjectsPageService : IProjectsPageService, IProjectsPageServiceInitializer
{
    private bool _initialized;
    private readonly ICurrentUserService _currentUserService;
    private readonly IAPIClient _client;

    public ProjectsPageService(
        ICurrentUserService currentUserService,
        IAPIClient client
        )
    {
        this._currentUserService = currentUserService;
        this._client = client;
    }
    
    public async Task<ObservableCollection<Project>> GetProjects() => await _client.GetProjects((int)_currentUserService.CurrentUser.UserId);

    public void Initialize()
    {
        if (_initialized)
        {
            throw new InvalidOperationException($"{nameof(IProjectsPageService)} is already initialized");
        }

        _initialized = true;
    }
}
