using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;

namespace ProjectManagementStudio.Model.TestModel;


public class TestModel : ITestModel, INotifyPropertyChanged
{

    private string _login;
    public string Login
    {
        get => _login;
        set
        {
            _login = value; OnPropertyChanged();
        }
    }
    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            _password = value; OnPropertyChanged();
        }
    }
    private string _email;
    public string Email
    {
        get => _email;
        set
        {
            Email = value; OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}