using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

[DataContract]
public class GetTagResponse
{
    [DataMember(Name = "success")]
    public bool Success { get; set; }

    [DataMember(Name = "code")]
    public int Code { get; set; }

    [DataMember(Name = "data")]
    public GetTagModel Data { get; set; }

    public GetTagResponse()
    {
        Success = false;
        Code = -1;
        Data = new();
    }
}
