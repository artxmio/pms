using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.PageServices.IStatisticPageService;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectManagementStudio.Model.Enums;
using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.Bootstrapper.Services.DocumentsGenerator;
using Microsoft.Win32;
using System.Windows;
using MaterialDesignThemes.Wpf;

namespace ProjectManagementStudio.Bootstrapper.Services.PageServices.StatisticPageService;

internal class StatisticPageService : IStatisticPageService, IStatisticPageServiceInitializer, INotifyPropertyChanged
{
    private bool _isInitialized;
    private ISeries[] _projectSeries = [];
    private ISeries[] _sprintSeries = [];
    private ISeries[] _userSeries = [];
    private readonly IAPIClient _client;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDocumentsGenerator _documentGenerator;

    public event PropertyChangedEventHandler? PropertyChanged;

    public ISeries[] ProjectSeries
    {
        get => _projectSeries;
        set
        {
            _projectSeries = value;
            OnPropertyChanged();
        }
    }

    public ISeries[] SprintSeries
    {
        get => _sprintSeries;
        set
        {
            _sprintSeries = value;
            OnPropertyChanged();
        }
    }

    public ISeries[] UserSeries
    {
        get => _userSeries;
        set
        {
            _userSeries = value;
            OnPropertyChanged();
        }
    }

    public Axis[] YAxesSprintSeries { get; set; } =
{
        new Axis
        {
            Name = "Количество спринтов",
            NameTextSize = 17,
            MinLimit = 0,
            TextSize = 14
        }
    };

    public Axis[] XAxesSprintSeries { get; set; } =
            [
                new Axis
                {
                    TextSize = 14,
                    LabelsPaint = null,
                    Labels = null
                }
            ];


    public StatisticPageService(IAPIClient client, ICurrentUserService currentUserService, IDocumentsGenerator documentsGenerator)
    {
        _client = client;
        _currentUserService = currentUserService;
        _documentGenerator = documentsGenerator;
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
        await InitializeProjectSeries();
        await InitializeSprintsTasksSeries();
        await InitializeProjectUserSeries();
    }

    public async Task ExportDocx()
    {
        var dialog = new SaveFileDialog
        {
            Title = "Выберите путь для сохранения",
            Filter = "Документ Word (*.docx)|*.docx"
        };

        if (dialog.ShowDialog() == true)
        {
            List<Project> projects = [.. await _client.GetProjects((int)_currentUserService.CurrentUser.UserId)];
            await _documentGenerator.GenerateDocx(projects, dialog.FileName);

            MessageBox.Show("Файл сохранён", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private async Task InitializeProjectUserSeries()
    {
        List<int> projectsIds = [.. (await _client.GetProjects((int)_currentUserService.CurrentUser.UserId)).Select(x => x.Id)];

        List<List<User>> users = [];
        foreach (var id in projectsIds)
        {
            users.Add([.. (await _client.GetProjectUsers(id))]);
        }

        _userSeries = [.. projectsIds.Select((id, index) => new LineSeries<int>
                                                {
                                                    Values = new List<int> { users[index].Count },
                                                    Name = $"Проект {id}",
                                                    Stroke = new SolidColorPaint(new SKColor(30, 144, 255), 2),
                                                    GeometrySize = 8,
                                                    Fill = null
                                                })];

        OnPropertyChanged(nameof(UserSeries));
    }

    private async Task InitializeSprintsTasksSeries()
    {
        List<int> projectsIds = [.. (await _client.GetProjects((int)_currentUserService.CurrentUser.UserId)).Select(x => x.Id)];

        List<List<Sprint>> sprints = [];
        foreach (var id in projectsIds)
        {
            sprints.Add([.. (await _client.GetSprintsByProjectID(id))]);
        }

        _sprintSeries = [.. projectsIds.Select((id, index) => new ColumnSeries<int>
        {
            Values = new List<int> { sprints[index].Count },
            Name = $"Проект {id}"
        })];

        OnPropertyChanged(nameof(SprintSeries));
        OnPropertyChanged(nameof(XAxesSprintSeries));
    }

    private async Task InitializeProjectSeries()
    {
        var projects = (await _client.GetProjects((int)_currentUserService.CurrentUser.UserId))
                            .GroupBy(p => p.Status)
                            .ToDictionary(g => g.Key, g => g.Count());

        var series = new List<ISeries>();

        foreach (var item in projects)
        {
            series.Add(new PieSeries<int>
            {
                Name = item.Key.ToString(),
                Values = [item.Value],
                Fill = new SolidColorPaint(GetStatusColor(item.Key)),
                Stroke = null,
                DataLabelsSize = 15,
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                MaxRadialColumnWidth = 60,
            });

        }

        _projectSeries = [.. series];

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
            _ => SKColors.Black
        };
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
