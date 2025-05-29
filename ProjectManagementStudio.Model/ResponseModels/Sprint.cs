using ProjectManagementStudio.Model.Enums;
using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

[DataContract]
public class Sprint
{
    [DataMember(Name="id")]
    public int Id { get; set; }
    [DataMember(Name = "start_date")] 
    public string StartDate { get; set; }
    [DataMember(Name = "end_date")] 
    public string EndDate { get; set; }
    [DataMember(Name = "status")]
    public SprintStatus Status { get; set; }

    public Sprint()
    {
        Id = -1;
        StartDate = "";
        EndDate = "";
        Status = SprintStatus.Unknown;
    }
}
