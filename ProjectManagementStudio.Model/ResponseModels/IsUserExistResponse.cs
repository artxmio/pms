using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.Responses;

[DataContract]
public class IsUserExistResponse
{
    [DataMember(Name = "exists")]
    public string Exists { get; set; }

    public IsUserExistResponse()
    {
        Exists = "";
    }
}

