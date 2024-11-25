namespace ProjectManagementStudio.ViewModel.UrlService;

public interface IUrlService
{
    string URLBase { get; set; }
    string Token { get; set; }

    void SetUrlCommand(string command);
}