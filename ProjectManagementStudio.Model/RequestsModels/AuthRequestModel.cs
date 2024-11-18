using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.RequestsModels;

[DataContract]
public class AuthRequestModel
{
    [DataMember(Name = "login")]
    public string Login { get; private set; }

    [DataMember(Name = "password")]
    public string Password { get; private set; }
    
    public AuthRequestModel(string login, string password)
    {
        Login = login;
        Password = password;
    }
}

