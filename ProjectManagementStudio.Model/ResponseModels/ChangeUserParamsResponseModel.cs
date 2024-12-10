using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

[DataContract]
public class ChangeUserParamsResponseModel
{
    [DataMember(Name = "success")]
    public bool Success { get; set; }

    [DataMember(Name = "code")]
    public int Code { get; set; }

    public ChangeUserParamsResponseModel()
    {
        Success = false;
        Code = -1;
    }
}
