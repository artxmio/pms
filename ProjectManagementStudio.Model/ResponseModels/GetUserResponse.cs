using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

[DataContract]
public class GetUserResponse
{
    [DataMember(Name = "success")]
    public bool Success { get; set; }

    [DataMember(Name = "code")]
    public int Code { get; set; }

    [DataMember(Name = "data")]
    public Dictionary<object, object> Data { get; set; }

    public GetUserResponse()
    {
        Success = false;
        Code = -1;
        Data = [];
    }
}