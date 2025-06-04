using ProjectManagementStudio.ViewModel.MenuWindow;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

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

    private void SavePieChartAsImage(UIElement pieChartElement, string filePath)
    {
        var renderBitmap = new RenderTargetBitmap(
            (int)pieChartElement.RenderSize.Width,
            (int)pieChartElement.RenderSize.Height,
            96, 96,
            PixelFormats.Pbgra32);

        renderBitmap.Render(pieChartElement);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
            encoder.Save(fileStream);
        }

        MessageBox.Show($"Диаграмма сохранена как изображение: {filePath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
