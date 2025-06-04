using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace ProjectManagementStudio.ViewModel.PageServices.IStatisticPageService;

public interface IStatisticPageService
{
    ISeries[] ProjectSeries { get; set; }
    ISeries[] SprintSeries { get; set; }
    ISeries[] UserSeries { get; set; }

    Axis[] YAxesSprintSeries { get; set; }
    Axis[] XAxesSprintSeries { get; set; }

    Task InitializeSeries();

    Task ExportDocx();
}
