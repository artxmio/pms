using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.ViewModel.MenuWindow;
using ProjectManagementStudio.ViewModel.PageServices.IProjectsPageService;
using System.Windows;

namespace ProjectManagementStudio.View.MenuWindow.pages;

public partial class ProjectPage : IProjectPage
{
    private readonly IMenuWindowViewModel _viewModel;

    public ProjectPage(IMenuWindowViewModel viewModel)
    {
        InitializeComponent();

        this._viewModel = viewModel;

        Loaded += ProjectPage_Loaded;
    }

    private void ProjectPage_Loaded(object sender, RoutedEventArgs e)
    {
        _viewModel.LoadProjectsCommand.Execute(this);
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement element && element.DataContext is Project project)
        {
            _viewModel.StopProjectCommand.Execute(project);
        }
    }
}
