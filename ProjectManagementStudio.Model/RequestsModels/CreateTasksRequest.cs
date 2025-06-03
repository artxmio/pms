using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.RequestsModels;

[DataContract]
public class CreateTasksRequest
{
    [DataMember(Name ="user_id")]
    public int UserId { get; set; }
    [DataMember(Name = "sprint_id")]
    public int SprintId { get; set; }
    [DataMember(Name ="tags_ids")]
    public List<int> TagsIds { get; set; }
    [DataMember(Name ="name")]
    public string TaskName {  get; set; }
    [DataMember(Name ="task_description")]
    public string TaskDescription { get; set; }

    public CreateTasksRequest()
    {
        UserId = -1;
        SprintId = -1;
        TagsIds = [];
        TaskName = "";
        TaskDescription = "";
    }
}
