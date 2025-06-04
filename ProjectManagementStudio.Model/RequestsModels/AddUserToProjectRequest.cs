using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.RequestsModels;

[DataContract]
public class AddUserToProjectRequest
{
    [DataMember(Name = "project_id")]
    public int ProjectId {  get; set; }
    [DataMember(Name = "user_id")]
    public int UserId { get; set; }

    public AddUserToProjectRequest()
    {
        ProjectId = -1;
        UserId = -1;
    }
}
