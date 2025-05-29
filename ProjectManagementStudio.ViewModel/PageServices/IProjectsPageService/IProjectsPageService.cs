using ProjectManagementStudio.Model.Enums;
using ProjectManagementStudio.Model.ResponseModels;
using System.Collections.ObjectModel;

namespace ProjectManagementStudio.ViewModel.PageServices.IProjectsPageService;

public interface IProjectsPageService
{
    Project SelectedProject { get; set; }
    Task<ObservableCollection<Project>> GetProjects();
    void OpenAddProjectWindow();
    Task ChangeStatus(int projectId, ProjectStatus projectStatus);

    Task<ObservableCollection<Sprint>> GetSprints(int projectId);
    Task<ObservableCollection<User>> GetProjectUsers(int projectId);
}