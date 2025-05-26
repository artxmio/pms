namespace ProjectManagementStudio.ViewModel.Pages;

public interface IPageManager
{
    IPage ActivePage { get; }

    IPage NavigateTo(Pages newPage);
    void GoBack();
}
