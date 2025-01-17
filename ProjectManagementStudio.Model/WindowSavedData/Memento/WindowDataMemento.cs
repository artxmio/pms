using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.WindowSavedData.Memento;

[DataContract]
public class WindowDataMemento : IWindowDataMemento
{
    [DataMember(Name = "width")]
    public int Width { get; set; }

    [DataMember(Name = "height")]
    public int Height { get; set; }

    public WindowDataMemento()
    {
        Width = 1056;
        Height = 600;
    }
}
