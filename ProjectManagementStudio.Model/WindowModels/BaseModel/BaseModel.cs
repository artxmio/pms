using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ProjectManagementStudio.Model.WindowModels.BaseModel;

public abstract class BaseModel : INotifyPropertyChanged
{
    protected readonly Regex _loginRegex = new("^[a-zA-Z0-9_]{6,20}$");
    protected readonly Regex _passwordRegex = new("^[a-zA-Z0-9@#$%&*()<>[\\]{}]{6,24}$");
    protected readonly Regex _emailRegex = new("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,50}$");

    public virtual string login { get; set; }
    public virtual string password { get; set; }
    public virtual string email { get; set; }

    protected bool _isValid = false;
    protected abstract void Validate();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

