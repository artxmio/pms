namespace ProjectManagementStudio.Model.WindowSavedData.Wrapper;

public interface IWindowDataMementoWrapper
{
    int Width { get; set; }
    int Height { get; set; }

    void SaveWindowData();
}