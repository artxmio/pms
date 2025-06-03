using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.Enums;
using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.AddProject;
using ProjectManagementStudio.ViewModel.PageServices.IProjectsPageService;
using ProjectManagementStudio.ViewModel.Windows;
using System.Collections.ObjectModel;

namespace ProjectManagementStudio.Bootstrapper.Services.PageServices.ProjectsPageService;

internal class ProjectsPageService : IProjectsPageService, IProjectsPageServiceInitializer
{
    private bool _initialized;
    private Project _selectedProject = null!;
    private Sprint _selectedSprint = null!;
    private SprintTask _selectedTask = null!;
    private readonly ICurrentUserService _currentUserService;
    private readonly IAPIClient _client;
    private readonly IWindowManager _windowManager;
    private readonly IAddProjectWindowViewModel _addProjectViewModel;

    public Project SelectedProject
    {
        get => _selectedProject; 
        set => _selectedProject = value;
    }

    public Sprint SelectedSprint
    {
        get => _selectedSprint;
        set => _selectedSprint = value;
    }

    public SprintTask SelectedTask
    {
        get => _selectedTask;
        set => _selectedTask = value;
    }


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
    public async Task<ObservableCollection<Sprint>> GetSprints(int projectId) => await _client.GetSprintsByProjectID(projectId);
    public async Task<ObservableCollection<User>> GetProjectUsers(int projectId) => await _client.GetProjectUsers(projectId);
    public async Task<ObservableCollection<SprintTask>> GetSprintTasks(int sprintId) => await _client.GetTasks(sprintId);
    public async Task<ObservableCollection<Tag>> GetTags() => await _client.GetTags();


    public void OpenAddProjectWindow()
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

    public async Task ChangeStatus(int projectId, ProjectStatus projectStatus)
    {
        await _client.ChangeProjectStatus(projectId, projectStatus);
    }

    public async Task CreateSprint(int projectId, int duration)
    {
        await _client.CreateSprint(projectId, duration);
    }

    public async Task CreateTask(int projectId, int userId, SprintTask task)
    {
        await _client.CreateTask(projectId, userId, task);
    }
}
