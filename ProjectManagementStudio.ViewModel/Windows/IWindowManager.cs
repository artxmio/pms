namespace ProjectManagementStudio.ViewModel.Windows;

public interface IWindowManager
{
    IWindow Show<T>(T viewModel, bool isDialog = false)
        where T : IWindowViewModel;

    void Close<T>(T viewModel)
        where T : IWindowViewModel;
}