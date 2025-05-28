using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows;
using ProjectManagementStudio.ViewModel.PageServices.IProjectsPageService;
using ProjectManagementStudio.ViewModel.Windows;
using System.Collections.ObjectModel;

namespace ProjectManagementStudio.Bootstrapper.Services.PageServices.ProjectsPageService;

internal class ProjectsPageService : IProjectsPageService, IProjectsPageServiceInitializer
{
    private bool _initialized;
    private readonly ICurrentUserService _currentUserService;
    private readonly IAPIClient _client;
    private readonly IWindowManager _windowManager;
    private readonly IAddProjectWindowViewModel _addProjectViewModel;

    public ProjectsPageService(
        ICurrentUserService currentUserService,
        IAPIClient client,
        IWindowManager windowManager,
        IAddProjectWindowViewModel addProjectWindowView
        )
    {
        this._currentUserService = currentUserService;
        this._client = client;
        this._windowManager = windowManager;
        this._addProjectViewModel = addProjectWindowView; 
    }
    
    public async Task<ObservableCollection<Project>> GetProjects() => await _client.GetProjects((int)_currentUserService.CurrentUser.UserId);

    public void AddProject()
    {
        _windowManager.Show(_addProjectViewModel, true);
    }

    public void Initialize()
    {
        if (_initialized)
        {
            throw new InvalidOperationException($"{nameof(IProjectsPageService)} is already initialized");
        }

        _initialized = true;
    }
}
