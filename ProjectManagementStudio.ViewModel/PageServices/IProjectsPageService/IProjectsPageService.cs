using ProjectManagementStudio.Model.Enums;
using ProjectManagementStudio.Model.ResponseModels;
using System.Collections.ObjectModel;

namespace ProjectManagementStudio.ViewModel.PageServices.IProjectsPageService;

public interface IProjectsPageService
{
    Project SelectedProject { get; set; }
    Sprint SelectedSprint {  get; set; }
    SprintTask SelectedTask { get; set; }

    Task<ObservableCollection<Project>> GetProjects();
    void OpenAddProjectWindow();
    Task ChangeStatus(int projectId, ProjectStatus projectStatus);
    Task CreateSprint(int projectId, int duration);
    Task CreateTask(int projectId, int userId, SprintTask task);

    Task<ObservableCollection<Sprint>> GetSprints(int projectId);
    Task<ObservableCollection<User>> GetProjectUsers(int projectId);
    Task<ObservableCollection<SprintTask>> GetSprintTasks(int sprintId);
    Task<ObservableCollection<Tag>> GetTags();
}