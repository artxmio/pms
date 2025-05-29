using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

public class GetProjectUsersResponse
{
    [DataMember(Name = "success")]
    public bool Success { get; set; }

    [DataMember(Name = "code")]
    public int Code { get; set; }

    [DataMember(Name = "data")]
    public List<User> Data { get; set; }

    public GetProjectUsersResponse()
    {
        Success = false;
        Code = -1;
        Data = [];
    }
}