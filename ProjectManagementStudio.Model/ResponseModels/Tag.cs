using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

[DataContract]
public class Tag
{
    [DataMember(Name = "id")]
    public int Id { get; set; }
    [DataMember(Name = "name")]
    public string TagName { get; set; }

    public Tag()
    {
        Id = -1;
        TagName = "";
    }
}
