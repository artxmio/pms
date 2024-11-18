using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using System.Text.RegularExpressions;

namespace ProjectManagementStudio.Model.WindowModels.AuthModel;

public class AuthModel : BaseModel.BaseModel, IAuthModel
{
    private readonly IUserDataMementoWrapper _wrapper;

    public override string login
    {
        get => _wrapper.UserLogin;
        set
        {
            _wrapper.UserLogin = value;
            OnPropertyChanged();
            Validate();
        }
    }
    public override string password
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

    protected override void Validate()
    {
        if (_loginRegex.IsMatch(login) && _passwordRegex.IsMatch(password))
            IsValid = true;
        else
            IsValid = false;
    }
}