using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.RequestsModels;

[DataContract]
public class AddUserRequestModel
{
    [DataMember(Name = "login")]
    public string Login { get; set; }

    [DataMember(Name = "password")]
    public string Password { get; set; }

    [DataMember(Name = "email")]
    public string Email { get; set; }

    public AddUserRequestModel(string login, string password, string email)
    {
        Login = login;
        Password = password;
        Email = email;
    }
}