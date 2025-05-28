using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

[DataContract]
public class ChangeProjectStatusResponse
{
    [DataMember(Name = "success")]
    public bool Success { get; set; }

    [DataMember(Name = "code")]
    public int Code { get; set; }

    public ChangeProjectStatusResponse()
    {
        Success = false;
        Code = -1;
    }
}
