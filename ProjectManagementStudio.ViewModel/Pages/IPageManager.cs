namespace ProjectManagementStudio.ViewModel.Pages;

public interface IPageManager
{
    IPage ActivePage { get; }

    IPage NavigateTo(int newPage);
    void GoBack();
}
