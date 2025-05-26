namespace ProjectManagementStudio.Model.ResponseModels;

public class Project
{
    public string Title { get; set; }
    public int HeadId { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Status { get; set; }
}