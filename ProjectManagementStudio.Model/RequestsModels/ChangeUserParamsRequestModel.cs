using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.RequestsModels;

[DataContract]
public class ChangeUserParamsRequestModel
{
    [DataMember(Name = "user_id")]
    public long UserId { get; set; }

    [DataMember(Name = "parametr")]
    public string Param { get; set; }

    [DataMember(Name = "new_value")]
    public string NewValue { get; set; }

    public ChangeUserParamsRequestModel(long userID, string param, string newValue)
    {
        UserId = userID;
        Param = param;
        NewValue = newValue;
    }
}
