using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectManagementStudio.Model.SettingSize;

public class WindowSizes : IWindowSizes
{
    public int Width { get; set; }

    public int Height { get; set; }

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
