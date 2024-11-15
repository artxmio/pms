using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.UserSavedData.Memento;

public interface IUserDataMemento
{
    string UserLogin { get; set; }
    string UserPassword { get; set; }
}