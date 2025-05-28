using ProjectManagementStudio.Model.Enums;
using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.RequestsModels;

[DataContract]
public class ChangeProjectStatusRequestModel
{
    [DataMember(Name = "project_id")]
    public int ProjectId { get; set; }
    [DataMember(Name = "status")]
    public ProjectStatus ProjectStatus { get; set; }

    public ChangeProjectStatusRequestModel(int projectId, ProjectStatus projectStatus)
    {
        ProjectId = projectId;
        ProjectStatus = projectStatus;
    }
}