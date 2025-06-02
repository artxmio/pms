using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

[DataContract]
public class CreateTaskResponse
{
    [DataMember(Name = "success")]
    public bool Success { get; set; }

    [DataMember(Name = "message")]
    public string Message { get; set; }

    [DataMember(Name = "tag_id")]
    public int TaskId { get; set; }

    public CreateTaskResponse()
    {
        Success = false;
        Message = " ";
        TaskId = -1;
    }
}
