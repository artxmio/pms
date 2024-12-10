using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectManagementStudio.Model.ModalWindowsModels.LoginChangeModel;

public class LoginChangeModel : ILoginChangeModel, INotifyPropertyChanged
{
    private string _newLogin = string.Empty;
    private string _password = string.Empty;

    public string NewLogin
    {
        get => _newLogin;
        set
        {
            _newLogin = value;
            OnPropertyChanged();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}