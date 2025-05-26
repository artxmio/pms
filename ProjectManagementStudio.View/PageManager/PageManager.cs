using ProjectManagementStudio.View.PageFactory;
using ProjectManagementStudio.ViewModel.Pages;

namespace ProjectManagementStudio.View.PageManager;

public class PageManager : IPageManager
{
    private readonly IPageFactory _pageFactory;
    private readonly Stack<IPage> _history;

    public required IPage _activePage;

    public IPage ActivePage
    {
        get => _activePage;
        set
        {
            _activePage = value;
        }
    }

    public PageManager(IPageFactory factory)
    {
        _history = new Stack<IPage>();
        _pageFactory = factory;
    }

    public IPage NavigateTo(Pages pages)
    {
        if (_activePage is not null)
            _history.Push(_activePage);

        ActivePage = _pageFactory.Create((Pages)pages);

        return ActivePage;
    }

    public void GoBack()
    {
        if (_history.Count > 0)
        {
            _activePage = _history.Pop();
        }
    }
}