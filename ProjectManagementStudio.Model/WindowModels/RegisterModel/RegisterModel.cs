namespace ProjectManagementStudio.Model.WindowModels.RegisterModel;

public class RegisterModel : BaseModel.BaseModel, IRegisterModel
{
    private string _login = "";
    private string _password = "";
    private string _email = "";

    public override string login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged();
            Validate();
        }
    }
    public override string password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
            Validate();
        }
    }
    public override string email
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

    protected override void Validate()
    {
        if (_loginRegex.IsMatch(login) && _passwordRegex.IsMatch(password) && _emailRegex.IsMatch(email))
            IsValid = true;
        else
            IsValid = false;
    }
}