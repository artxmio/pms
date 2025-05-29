using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

public class GetSprintsResponse
{
    [DataMember(Name = "success")]
    public bool Success { get; set; }

    [DataMember(Name = "code")]
    public int Code { get; set; }

    [DataMember(Name = "data")]
    public List<Sprint> Data { get; set; }

    public GetSprintsResponse()
    {
        Success = false;
        Code = -1;
        Data = [];
    }
}
