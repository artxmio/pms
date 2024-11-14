using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ProjectManagementStudio.Model.AuthModel;

public class AuthModel : IAuthModel, INotifyPropertyChanged
{
    private readonly Regex _loginRegex = new Regex("^[a-zA-Z0-9_]{6,20}$");
    private readonly Regex _passwordRegex = new("^[a-zA-Z0-9@#$%&*()<>[\\]{}]{6,24}$");

    private readonly IUserDataMementoWrapper _wrapper;

    private string _email = "";
    private bool _isValid = false;

    public string login
    {
        get => _wrapper.UserLogin;
        set
        {
            _wrapper.UserLogin = value;
            OnPropertyChanged();
            Validate();
        }
    }
    public string password
    {
        get => _wrapper.UserPassword;
        set
        {
            _wrapper.UserPassword = value;
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

    public AuthModel(IUserDataMementoWrapper wrapper)
    {
        _wrapper = wrapper;
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