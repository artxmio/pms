using ProjectManagementStudio.Model.ModalWindowsModels.LoginChangeModel;
using ProjectManagementStudio.ViewModel.Windows;

namespace ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.ChangeLogin;

public interface ILoginChangeDialogViewModel : IWindowViewModel
{
    public ILoginChangeModel LoginChangeModel { get; set; }
}