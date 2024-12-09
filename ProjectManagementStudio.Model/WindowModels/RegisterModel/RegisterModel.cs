namespace ProjectManagementStudio.Model.WindowModels.RegisterModel;

public class RegisterModel : BaseModel.BaseModel, IRegisterModel
{
    private string _login = "";
    private string _password = "";
    private string _email = "";

    public override string Login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged();
            Validate();
        }
    }
    public override string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
            Validate();
        }
    }
    public override string Email
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

    public void Validate()
    {
        if (_loginRegex.IsMatch(Login) && _passwordRegex.IsMatch(Password) && _emailRegex.IsMatch(Email))
            IsValid = true;
        else
            IsValid = false;
    }
}