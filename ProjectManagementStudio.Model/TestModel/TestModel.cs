using System.ComponentModel;

namespace ProjectManagementStudio.Model.TestModel;

public class TestModel : ITestModel
{
    public string Login { get; set; } = "";
    public string Password { get; set; } = "";
    public string Email { get; set; } = "";
}   