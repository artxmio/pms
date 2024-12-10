using ProjectManagementStudio.Model.UserSavedData.Wrapper;

namespace ProjectManagementStudio.Model.WindowModels.AuthModel;

public class AuthModel : BaseModel.BaseModel, IAuthModel
{
    private readonly IUserDataMementoWrapper _wrapper;

    public override string Login
    {
        get => _wrapper.UserLogin;
        set
        {
            _wrapper.UserLogin = value;
            OnPropertyChanged();
            Validate();
        }
    }
    public override string Password
    {
        get => _wrapper.UserPassword;
        set
        {
            _wrapper.UserPassword = value;
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
    public bool IsRememberMe
    {
        get => _wrapper.IsRememberMe;

        set
        {
            _wrapper.IsRememberMe = value;
            OnPropertyChanged();
        }
    }

    public AuthModel(IUserDataMementoWrapper wrapper)
    {
        _wrapper = wrapper;
    }

    public void Validate()
    {
        if (_loginRegex.IsMatch(Login) && _passwordRegex.IsMatch(Password))
            IsValid = true;
        else
            IsValid = false;
    }
}