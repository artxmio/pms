using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

[DataContract]
public class GetTasksResponse
{
    [DataMember(Name = "success")]
    public bool Success { get; set; }

    [DataMember(Name = "code")]
    public int Code { get; set; }

    [DataMember(Name = "data")]
    public List<SprintTask> Data { get; set; }

    public GetTasksResponse()
    {
        Success = false;
        Code = -1;
        Data = [];
    }
}