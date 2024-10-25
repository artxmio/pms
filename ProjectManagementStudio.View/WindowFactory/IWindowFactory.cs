using ProjectManagementStudio.ViewModel.Windows;

namespace ProjectManagementStudio.View.WindowFactory;

public interface IWindowFactory
{
    public IWindow Create<T>(T viewModel)
    where T : IWindowViewModel;
}

