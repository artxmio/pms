using LiveChartsCore;
using System.Collections.ObjectModel;

namespace ProjectManagementStudio.ViewModel.PageServices.IStatisticPageService;

public interface IStatisticPageService
{
    ObservableCollection<ISeries> ProjectSeries { get; set; }

    Task InitializeSeries();
}
