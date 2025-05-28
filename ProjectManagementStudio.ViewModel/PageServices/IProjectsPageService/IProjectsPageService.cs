using ProjectManagementStudio.Model.Enums;
using ProjectManagementStudio.Model.ResponseModels;
using System.Collections.ObjectModel;

namespace ProjectManagementStudio.ViewModel.PageServices.IProjectsPageService;

public interface IProjectsPageService
{
    Task<ObservableCollection<Project>> GetProjects();
    void OpenAddProjectWindow();
    Task ChangeStatus(int projectId, ProjectStatus projectStatus);
}