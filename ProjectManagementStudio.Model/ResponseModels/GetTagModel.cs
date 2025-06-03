using System.Runtime.Serialization;

namespace ProjectManagementStudio.Model.ResponseModels;

[DataContract]
public class GetTagModel
{
    [DataMember(Name = "id")]
    public int Id { get; set; }
    [DataMember(Name = "tag_name")]
    public string TagName { get; set; }
    [DataMember(Name = "description")]
    public string TagDescription { get; set; }

    public GetTagModel()
    {
        Id = -1;
        TagName = "";
        TagDescription = "";
    }
}
