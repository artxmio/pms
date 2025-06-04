using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

public class AddUserToProjectResponse
{
    [DataMember(Name = "success")]
    public bool Success { get; set; }

    [DataMember(Name = "message")]
    public string Message { get; set; }
    
    public AddUserToProjectResponse()
    {
        Success = false;
        Message = " ";
    }
}
