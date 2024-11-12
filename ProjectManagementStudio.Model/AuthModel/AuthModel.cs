using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ProjectManagementStudio.Model.AuthModel;

public class AuthModel : IAuthModel, INotifyPropertyChanged
{
    private readonly Regex _loginRegex = new Regex("^[a-zA-Z0-9_]{6,20}$");
    private readonly Regex _passwordRegex = new("^[a-zA-Z0-9@#$%&*()<>[\\]{}]{6,24}$");

    private string _login = "";
    private string _password = "";
    private string _email = "";
    private bool _isValid = false;

    public string login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged();
            Validate();
        }
    }
    public string password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
            Validate();
        }
    }
    public string email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
            Validate(); 
        }
    }
    public bool IsValid
    {
        get => _isValid;
        set
        {
            _isValid = value;
            OnPropertyChanged();
        }
    }

    private void Validate()
    {
        if (_loginRegex.IsMatch(login) && _passwordRegex.IsMatch(password))
            IsValid = true;
        else
            IsValid = false;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}