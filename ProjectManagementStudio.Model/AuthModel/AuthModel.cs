using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectManagementStudio.Model.AuthModel;

public class AuthModel : IAuthModel, INotifyPropertyChanged
{
    private string _login = "";
    private string _password = "";
    private string _email = "";

    public string login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged();
        }
    }
    public string password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }
    public string email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}