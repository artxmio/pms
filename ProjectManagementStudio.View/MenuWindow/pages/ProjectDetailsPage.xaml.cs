using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.ViewModel.MenuWindow;
using System.Windows;
using System.Windows.Controls;

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

    private void ShowPopup(object sender, RoutedEventArgs e)
    {
        SprintPopup.IsOpen = true;
    }

    private void ClosePopup(object sender, RoutedEventArgs e)
    {
        ValidateDate(DatePicker);
    }

    private void ValidateDate(DatePicker picker)
    {
        if (picker.SelectedDate < DateTime.Now)
        {
            ErrorPopup.IsOpen = true;
        }
        else
        {
            ErrorPopup.IsOpen = false;
            SprintPopup.IsOpen = false;
        }
    }

    private void UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UserPopup.IsOpen = true;
    }

    private void CloseUserPopup(object sender, RoutedEventArgs e)
    {
        UserPopup.IsOpen = false;
    }
}
