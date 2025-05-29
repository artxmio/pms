using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.RequestsModels;

[DataContract]
public class GetProjectUsersRequest
{
    [DataMember(Name ="project_id")]
    public int ProjectId { get; set; }

    public GetProjectUsersRequest(int projectId)
    {
        ProjectId = projectId;
    }
}
