using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.UserSavedData.Memento;

[DataContract]
public class UserDataMemento : IUserDataMemento
{
    [DataMember(Name = "userLogin")]
    public string UserLogin { get; set; }

    [DataMember(Name = "userPassword")]
    public string UserPassword { get; set; }

    [DataMember(Name = "isRememberMe")]
    public bool IsRememberMe { get; set; }

    [DataMember(Name = "aboutText")]
    public string AboutText { get; set; }

    public UserDataMemento()
    {
        UserLogin = ""; 
        UserPassword = "";
        IsRememberMe = false;
        AboutText = "";
    }
}