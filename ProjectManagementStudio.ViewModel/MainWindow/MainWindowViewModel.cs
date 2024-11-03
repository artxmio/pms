using ProjectManagementStudio.Model.TestModel;

namespace ProjectManagementStudio.ViewModel.MainWindow;

public class MainWindowViewModel : IMainWindowViewModel
{
    public TestModel test { get; set; }

    public MainWindowViewModel()
    {
        test = new();
    }
}