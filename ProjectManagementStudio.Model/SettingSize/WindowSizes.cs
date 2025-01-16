namespace ProjectManagementStudio.Model.SettingSize;

public class WindowSizes : IWindowSizes
{
    public int Width { get; }

    public int Height { get; }

    public WindowSizes(int width, int heigt)
    {
        this.Width = width;
        this.Height = heigt;
    }

    public override string ToString()
    {
        return $"{this.Width}x{this.Height}";
    }
}
