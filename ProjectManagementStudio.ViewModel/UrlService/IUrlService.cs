namespace ProjectManagementStudio.ViewModel.UrlService;

public interface IUrlService
{
    string Url { get; }
    void SetUrlCommand(string command);
}