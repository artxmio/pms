using ProjectManagementStudio.Model.Enums;
using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

[DataContract]
public class Project
{
    [DataMember(Name = "id")]
    public int Id { get; set; }
    [DataMember(Name = "title")]
    public string Title { get; set; }
    [DataMember(Name = "role")]
    public bool HeadId { get; set; }
    [DataMember(Name = "description")]
    public string Description { get; set; }

    [DataMember(Name = "start_date")]
    public string StartDate { get; set; }
    [DataMember(Name = "end_date")]
    public string EndDate { get; set; }

    [DataMember(Name = "status")]
    public ProjectStatus Status { get; set; }

    public Project()
    {
        Title = "";
        Description = "";
        StartDate = "";
        EndDate = "";
    }
}