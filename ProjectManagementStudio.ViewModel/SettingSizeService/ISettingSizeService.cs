using ProjectManagementStudio.Model.SettingSize;
using System.Collections.ObjectModel;

namespace ProjectManagementStudio.ViewModel.SettingSizeService;

public interface ISettingSizeService
{
    public IWindowSizes Current { get; set; }
    public ObservableCollection<IWindowSizes> Sizes { get; set; }

    void UpdateWindowSize(IWindowSizes newSize);
}
