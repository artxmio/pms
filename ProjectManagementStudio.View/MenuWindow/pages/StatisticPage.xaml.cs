using ProjectManagementStudio.ViewModel.MenuWindow;

namespace ProjectManagementStudio.View.MenuWindow.pages;

public partial class StatisticPage : IStatisticPage
{
    private readonly IMenuWindowViewModel _viewModel;

    public StatisticPage(IMenuWindowViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        Loaded += StatisticPage_Loaded;
    }

    private void StatisticPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        _viewModel.LoadSeriesCommand.Execute(null);
    }
}
