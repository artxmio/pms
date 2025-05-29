using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

[DataContract]
public class User
{
    [DataMember(Name = "user_id")]
    public int UserId { get; set; }
    [DataMember(Name = "username")]
    public string UserName { get; set; }
    [DataMember(Name ="role")]
    public bool Role { get; set; }

    public User()
    {
        UserId = 0;
        UserName = "";
        Role = false;
    }
}
