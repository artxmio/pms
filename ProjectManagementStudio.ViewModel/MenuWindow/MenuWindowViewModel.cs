using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Pages;
using ProjectManagementStudio.ViewModel.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.MenuWindow;

public class MenuWindowViewModel : IMenuWindowViewModel, INotifyPropertyChanged
{
    private readonly IPageManager _pageManager;
    private readonly IWindowManager _windowManager;

    private IPage _activePage;

    public IPage ActivePage
    {
        get => _activePage;
        set
        {
            _activePage = value;
            OnPropertyChanged();
        }
    }

    public ICommand CloseCommand { get; }

    public ICommand NavigateToProfilePage { get; }
    public ICommand NavigateToWelcomePage { get; }
    public ICommand NavigateToSettingsPage { get; }

    public MenuWindowViewModel(
        IWindowManager windowManager, 
        IPageManager pageManager)
    {
        _pageManager = pageManager;
        _windowManager = windowManager;

        _activePage = _pageManager.NavigateTo(2);

        CloseCommand = new RelayCommand(() => _windowManager.Close(this));
        NavigateToWelcomePage = new RelayCommand(() => ActivePage = _pageManager.NavigateTo(2));
        NavigateToProfilePage = new RelayCommand(() => ActivePage = _pageManager.NavigateTo(3));
        NavigateToSettingsPage = new RelayCommand(() => ActivePage = _pageManager.NavigateTo(4));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}