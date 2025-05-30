using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

    [DataContract]
public class CreateSprintResponse
{
    [DataMember(Name = "success")]
    public bool Success { get; set; }

    [DataMember(Name = "message")]
    public string Message { get; set; }

    public CreateSprintResponse()
    {
        Success = false;
        Message = " ";
    }
}
