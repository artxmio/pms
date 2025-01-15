using ProjectManagementStudio.ViewModel.WindowMediator;
using ProjectManagementStudio.ViewModel.Windows;

namespace ProjectManagementStudio.ViewModel;

public interface IWindowMediator
{
    void Notify(IWindowViewModel viewModel);
}