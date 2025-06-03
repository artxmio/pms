using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.PageServices.IStatisticPageService;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ProjectManagementStudio.Model.Enums;

namespace ProjectManagementStudio.Bootstrapper.Services.PageServices.StatisticPageService;

internal class StatisticPageService : IStatisticPageService, IStatisticPageServiceInitializer, INotifyPropertyChanged
{
    private bool _isInitialized;
    private ObservableCollection<ISeries> _projectSeries = new();
    private readonly IAPIClient _client;
    private readonly ICurrentUserService _currentUserService;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<ISeries> ProjectSeries
    {
        get => _projectSeries;
        set
        {
            _projectSeries = value;
            OnPropertyChanged();
        }
    }

    public StatisticPageService(IAPIClient client, ICurrentUserService currentUserService)
    {
        _client = client;
        _currentUserService = currentUserService;
    }

    public void Initialize()
    {
        if (_isInitialized)
        {
            throw new ApplicationException($"{nameof(StatisticPageService)} is already initialized");
        }
        _isInitialized = true;
    }

    public async Task InitializeSeries()
    {
        var projects = (await _client.GetProjects((int)_currentUserService.CurrentUser.UserId))
                            .GroupBy(p => p.Status)
                            .ToDictionary(g => g.Key, g => g.Count());

        _projectSeries.Clear(); 

        foreach (var item in projects)
        {
            _projectSeries.Add(new PieSeries<int>
            {
                Name = item.Key.ToString(),
                Values = [item.Value],
                Fill = new SolidColorPaint(GetStatusColor(item.Key)),
                Stroke = new SolidColorPaint(SKColors.Black) { StrokeThickness = 1 },
                InnerRadius = 70,
                DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                DataLabelsSize = 22,
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                DataLabelsFormatter = point => point.Coordinate.PrimaryValue.ToString("N2")
            });
        }

        OnPropertyChanged(nameof(ProjectSeries));
    }

    private SKColor GetStatusColor(ProjectStatus status)
    {
        return status switch
        {
            ProjectStatus.Unknown => SKColors.Gray,
            ProjectStatus.Opened => SKColors.Blue,
            ProjectStatus.Closed => SKColors.Green,
            ProjectStatus.Stoped => SKColors.Red,
            _ => SKColors.Black // Цвет по умолчанию
        };
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
