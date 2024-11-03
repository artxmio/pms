using System.ComponentModel;

namespace ProjectManagementStudio.Model.TestModel;

public class TestModel : ITestModel
{
    private string _login = "";
    private string _password = "";
    private string _email = "";

    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}   