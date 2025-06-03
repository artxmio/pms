using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.RequestsModels;

[DataContract]
public class ChangeTaskRequest
{
    [DataMember(Name ="task_id")]
    public int TaskId { get; set; }
    [DataMember(Name = "name")]
    public string TaskName { get; set; }
    [DataMember(Name = "discription")]
    public string TaskDescription { get; set; }
    [DataMember(Name ="tags")]
    public List<int> Tags { get; set; }

    public ChangeTaskRequest()
    {
        TaskId = -1;
        TaskName = "";
        TaskDescription = "";
        Tags = [];
    }
}
