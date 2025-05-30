using ProjectManagementStudio.ViewModel.MenuWindow;
using System.ComponentModel.Design;

namespace ProjectManagementStudio.View.MenuWindow.pages;

public partial class SprintDetailsPage : ISprintDetailsPage
{
    private readonly IMenuWindowViewModel _viewModel;

    public SprintDetailsPage(IMenuWindowViewModel viewModel)
    {
        InitializeComponent();

        this._viewModel = viewModel;

        Loaded += SprintDetailsPage_Loaded;
    }

    private void SprintDetailsPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        _viewModel.LoadSprintTasksCommand.Execute(this);
    }
}
