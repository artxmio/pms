using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

[DataContract]
public class SprintTask
{
    [DataMember(Name ="id")]
    public int Id { get; set; }
    [DataMember(Name ="task_name")]
    public string TaskName {  get; set; }
    [DataMember(Name ="description")]
    public string TaskDescription { get; set; }
    [DataMember(Name ="tags")]
    public List<Tag> Tags { get; set; }

    public SprintTask()
    {
        Id = -1;
        TaskName = "";
        TaskDescription = "";
        Tags = [];
    }
}
