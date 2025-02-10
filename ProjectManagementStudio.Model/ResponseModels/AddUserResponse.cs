using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

[DataContract]
public class AddUserResponse
{
    [DataMember(Name = "success")]
    public bool Success { get; set; }

    [DataMember(Name = "code")]
    public int Code { get; set; }

    [DataMember(Name = "message")]
    public string Message { get; set; }

   

    public AddUserResponse()
    {
        Success = false;
        Code = -1;
        Message = " ";
    }
}