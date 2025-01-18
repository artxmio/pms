using ProjectManagementStudio.ViewModel.MenuWindow;
using ProjectManagementStudio.ViewModel.SettingSizeService;
using System.Windows;
using System.Windows.Input;

namespace ProjectManagementStudio.View.MenuWindow;

public partial class MenuWindow : IMenuWindow
{
    private readonly IMenuWindowViewModel _viewModel;
    private readonly ISettingSizeService _settingSizeService;

    public MenuWindow(IMenuWindowViewModel viewModel, ISettingSizeService settingSizeService)
    {
        InitializeComponent();

        _viewModel = viewModel;
        _settingSizeService = settingSizeService;

        Loaded += OnLoad;
        MouseDoubleClick += DoubleClick;
    }

    private void DragWindow(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            this.DragMove();
        }
    }

    private void DoubleClick(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Maximized;
    }

    private void OnLoad(object sender, RoutedEventArgs e)
    {
        this.Width = _settingSizeService.Current.Width;
        this.Height = _settingSizeService.Current.Height;
     }
}