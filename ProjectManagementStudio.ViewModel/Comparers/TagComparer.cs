using ProjectManagementStudio.Model.ResponseModels;

namespace ProjectManagementStudio.ViewModel.Comparers;

public class TagComparer : IEqualityComparer<Tag>
{
    public bool Equals(Tag x, Tag y) => x.TagName == y.TagName;
    public int GetHashCode(Tag obj) => obj.Id.GetHashCode();
}
