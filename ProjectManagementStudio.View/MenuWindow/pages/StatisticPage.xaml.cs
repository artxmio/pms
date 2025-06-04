using ProjectManagementStudio.ViewModel.MenuWindow;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using LiveChartsCore.SkiaSharpView.WPF;

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

    private void SaveDiagrams(object sender, RoutedEventArgs e)
    {
        SaveChart(PieChart, "PieChart");
        SaveChart(CartesianChart, "CartesianChart");
        SaveChart(LinearChart, "LinearChart");

        PieChart.ApplyTemplate();
        PieChart.UpdateLayout();

        CartesianChart.ApplyTemplate();
        CartesianChart.UpdateLayout();

        LinearChart.ApplyTemplate();
        LinearChart.UpdateLayout();
    }

    private void SaveChart(UIElement element, string fileName)
    {
        if (element is Chart chart)
        {
            chart.ApplyTemplate();
            chart.UpdateLayout();

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                dc.DrawRectangle(new VisualBrush(chart), null, new Rect(new Size(chart.ActualWidth, chart.ActualHeight)));
            }

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(
                (int)chart.ActualWidth, (int)chart.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(dv);

            string directoryPath = "Temp";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            using FileStream fileStream = new FileStream($"{directoryPath}/{fileName}.png", FileMode.Create);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            encoder.Save(fileStream);
        }
    }
}
