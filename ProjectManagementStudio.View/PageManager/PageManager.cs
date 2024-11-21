using ProjectManagementStudio.View.PageFactory;
using ProjectManagementStudio.ViewModel.Pages;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectManagementStudio.View.PageManager;

public class PageManager : IPageManager
{
    private readonly IPageFactory _pageFactory;
    private readonly Stack<IPage> _history;

    public IPage _activePage;

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

        _activePage = factory.Create(0);
    }

    public IPage NavigateTo(int pages)
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