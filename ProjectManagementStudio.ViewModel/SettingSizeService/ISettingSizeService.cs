using ProjectManagementStudio.Model.SettingSize;
using System.Collections.ObjectModel;

namespace ProjectManagementStudio.ViewModel.SettingSizeService;

public interface ISettingSizeService
{
    public int Width { get; set; }
    public int Height { get; set; }
    public IWindowSizes SelectedWindowSize { get; set; }
    public ObservableCollection<IWindowSizes> Sizes { get; set; }

    void ApplySettings();
    void UpdateWindowSize(IWindowSizes newSize);
}
