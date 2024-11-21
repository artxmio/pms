using ProjectManagementStudio.ViewModel.Pages;

namespace ProjectManagementStudio.View.PageFactory;

public interface IPageFactory
{
    IPage Create(Pages pageKey);
}