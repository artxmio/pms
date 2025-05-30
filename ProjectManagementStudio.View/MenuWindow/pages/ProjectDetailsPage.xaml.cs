using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.ViewModel.MenuWindow;
using System.Windows;

namespace ProjectManagementStudio.View.MenuWindow.pages;

public partial class ProjectDetailsPage : IProjectDetailsPage
{
    private readonly IMenuWindowViewModel _viewModel;

    public ProjectDetailsPage(IMenuWindowViewModel viewModel)
    {
        InitializeComponent();

        this._viewModel = viewModel;

        Loaded += ProjectDetailsPage_Loaded;
    }

    private void ProjectDetailsPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        _viewModel.LoadSprintsCommand.Execute(null);
        _viewModel.LoadProjectUsersCommand.Execute(null);
    }

    private void StackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (sender is FrameworkElement element && element.DataContext is Sprint sprint)
        {
            _viewModel.NavigateToSprintDetailsCommand.Execute(sprint);
        }
    }
}
