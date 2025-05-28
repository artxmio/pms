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
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [DataMember(Name = "status")]
    public int Status { get; set; }
}