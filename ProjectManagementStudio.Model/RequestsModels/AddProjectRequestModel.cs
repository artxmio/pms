using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.RequestsModels;

[DataContract]
public class AddProjectRequestModel
{
    [DataMember(Name = "project_description")]
    public string ProjectDescription { get; set; }
    [DataMember(Name = "project_title")]
    public string ProjectTitle { get; set; }
    [DataMember(Name = "user_id")]
    public int UserId { get; set; }

    public AddProjectRequestModel()
    {
        ProjectTitle = "";
        ProjectDescription = "";
        UserId = 0;
    }
}
