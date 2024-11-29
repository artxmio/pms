using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.UserSavedData.Memento;

[DataContract]
public class UserDataMemento : IUserDataMemento, IUserImageMementoWrapper
{
    [DataMember(Name = "userLogin")]
    public string UserLogin { get; set; }

    [DataMember(Name = "userPassword")]
    public string UserPassword { get; set; }

    public Uri? AvatarImage { get; set; }

    public UserDataMemento()
    {
        UserLogin = ""; 
        UserPassword = "";
    }
}