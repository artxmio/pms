using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

[DataContract]
public class ProjectResponse
{
    [DataMember(Name = "Success")]
    public bool Success { get; set; }

    [DataMember(Name = "Data")]
    public List<Project> Data { get; set; }

    public ProjectResponse()
    {
        Success = false;
        Data = [];
    }
}
