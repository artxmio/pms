using ProjectManagementStudio.Model.ModalWindowsModels.LoginChangeModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.ChangeLogin;

public class LoginChangeDialogViewModel : ILoginChangeDialogViewModel, INotifyPropertyChanged
{
    private ILoginChangeModel _loginChangeModel;

    public ILoginChangeModel LoginChangeModel
    {
        get => _loginChangeModel;
        set
        {
            _loginChangeModel = value;
            OnPropertyChanged();
        }
    }

    public LoginChangeDialogViewModel(ILoginChangeModel loginChangeModel)
    {
        _loginChangeModel = loginChangeModel;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
