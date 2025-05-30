using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.RequestsModels;

[DataContract]
public class CreateSprintRequest
{
    [DataMember(Name ="project_id")]
    public int ProjectId { get; set; }
    [DataMember(Name = "sprint_duration")]
    public int SprintDuration { get; set; }

    public CreateSprintRequest()
    {
        ProjectId = -1;
        SprintDuration = -1;
    }
}
