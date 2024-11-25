using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.Responses;

[DataContract]
public class IsUserExistResponse
{
    [DataMember(Name = "Success")]
    public bool Success { get; set; }

    [DataMember(Name = "code")]
    public int Code { get; set; }

    [DataMember(Name = "data")]
    public string Data { get; set; }

    public IsUserExistResponse()
    {
        Success = false;
        Code = -1;
        Data = "";
    }
}

