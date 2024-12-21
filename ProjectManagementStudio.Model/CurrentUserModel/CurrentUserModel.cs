using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectManagementStudio.Model.CurrentUserModel;

public class CurrentUserModel : ICurrentUserModel, INotifyPropertyChanged
{
    private long _userId = 0;
    private string _userLogin = "";
    private string _userPassword = "";
    private string _userEmail = "";

    public long UserId
    {
        get
        {
            return _userId;
        }
        set
        {
            _userId = value;
            OnPropertyChanged();
        }
    }
    public string Login
    {
        get
        {
            return _userLogin;
        }
        set
        {
            _userLogin = value;
            OnPropertyChanged();
        }
    }
    public string Password
    {
        get
        {
            return _userPassword;
        }
        set
        {
            _userPassword = value;
            OnPropertyChanged();
        }
    }
    public string Email
    {
        get
        {
            return _userEmail;
        }
        set
        {
            _userEmail = value;
            OnPropertyChanged();
        }
    }

    public CurrentUserModel()
    {
        UserId = -1;
        Login = string.Empty;
        Password = string.Empty;
        Email = string.Empty;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
