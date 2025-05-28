using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.Model.SettingSize;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Pages;
using ProjectManagementStudio.ViewModel.PageServices.IProfilePageService;
using ProjectManagementStudio.ViewModel.PageServices.IProjectsPageService;
using ProjectManagementStudio.ViewModel.PageServices.ISettingsPageService;
using ProjectManagementStudio.ViewModel.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ProjectManagementStudio.ViewModel.MenuWindow;

public class MenuWindowViewModel : IMenuWindowViewModel, INotifyPropertyChanged
{
    /* // Поля // */
    #region

    private readonly IPageManager _pageManager;
    private readonly IWindowManager _windowManager;

    private readonly ICurrentUserService _currentUserService;
    private readonly IProfilePageService _profilePageService;
    private IPage _activePage;
    private ObservableCollection<Project> _projects = [];
    private readonly ISettingsPageService _settingPageService;
    private readonly IProjectsPageService _projectPageService;

    #endregion

    /* // Свойства // */
    #region

    public ICurrentUserModel CurrentUser
    {
        get
        {
            return _currentUserService.CurrentUser;
        }
        set
        {
            _currentUserService.CurrentUser = value;

            OnPropertyChanged();
        }
    }

    public IPage ActivePage
    {
        get => _activePage;
        set
        {
            _activePage = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<IWindowSizes> Sizes
    {
        get
        {
            return _settingPageService.SettingSizeService.Sizes;
        }
    }

    public IWindowSizes SelectedWindowSize
    {
        get
        {
            return _settingPageService.SettingSizeService.SelectedWindowSize;
        }
        set
        {
            _settingPageService.SettingSizeService.SelectedWindowSize = value;
            OnPropertyChanged();
        }
    }

    public int Width
    {
        get
        {
            return _settingPageService.SettingSizeService.Width;
        }
        set
        {
            _settingPageService.SettingSizeService.Width = value;
            OnPropertyChanged();
        }
    }

    public int Heigth
    {
        get
        {
            return _settingPageService.SettingSizeService.Width;
        }
        set
        {
            _settingPageService.SettingSizeService.Height = value;
            OnPropertyChanged();
        }
    }


    public ObservableCollection<Project> Projects
    {
        get => _projects;
        set
        {
            _projects = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(MyProjects));
        }
    }

    public ObservableCollection<Project> MyProjects
    {
        get => [.. _projects.Where(x => x.HeadId)];
    }
    #endregion

    /* // Команды // */
    #region 

    public ICommand CloseCommand { get; }
    public ICommand RollCommand { get; }
    public ICommand RestoreCommand { get; }

    public ICommand NavigateToProfilePage { get; }
    public ICommand NavigateToWelcomePage { get; }
    public ICommand NavigateToSettingsPage { get; }
    public ICommand NavigateToProjectPage { get; }

    public ICommand ChangeLoginCommand { get; }
    public ICommand ChangePasswordCommand { get; }
    public ICommand ChangeEmailCommand { get; }
    public ICommand ChangeAboutTextCommand { get; }

    public ICommand ChangeLocalizationCommand { get; }
    public ICommand ApplySettingsСommand { get; }

    public ICommand LogOutCommand { get; }

    public ICommand LoadProjectsCommand { get; }
    public ICommand AddProjectCommand { get; }
    public ICommand StopProjectCommand { get; }
    public ICommand CloseProjectCommand { get; }
    public ICommand OpenProjectCommand { get; }
    #endregion

    public MenuWindowViewModel(
        IWindowManager windowManager,
        IPageManager pageManager,
        ICurrentUserService currentUserService,
        IProfilePageService profilePageService,
        ISettingsPageService settingPageService,
        IProjectsPageService projectsPageService)
    {
        _pageManager = pageManager;
        _windowManager = windowManager;
        _currentUserService = currentUserService;

        _profilePageService = profilePageService;
        _settingPageService = settingPageService;
        _projectPageService = projectsPageService;

        _activePage = _pageManager.NavigateTo(Pages.Pages.WelcomePage);

        CloseCommand = new RelayCommand(o => _windowManager.Close(this));
        RollCommand = new RelayCommand(o => _windowManager.RollWindow(this));
        RestoreCommand = new RelayCommand(o => _windowManager.RestoreWindow(this));

        NavigateToWelcomePage = new RelayCommand(o => ActivePage = _pageManager.NavigateTo(Pages.Pages.WelcomePage));
        NavigateToProfilePage = new RelayCommand(o => ActivePage = _pageManager.NavigateTo(Pages.Pages.ProfilePage));
        NavigateToSettingsPage = new RelayCommand(o => ActivePage = _pageManager.NavigateTo(Pages.Pages.SettingsPage));
        NavigateToProjectPage = new RelayCommand(o => ActivePage = _pageManager.NavigateTo(Pages.Pages.ProjectPage));

        // Project's functions //
        #region

        LoadProjectsCommand = new RelayCommand(async o => { Projects = await _projectPageService.GetProjects(); });
        AddProjectCommand = new RelayCommand(o => _projectPageService.OpenAddProjectWindow());
        
        StopProjectCommand = new RelayCommand(async o =>
        {
            if (o is Project project)
            {
                await _projectPageService.ChangeStatus(project.Id, Model.Enums.ProjectStatus.Stoped);
                _projects = await _projectPageService.GetProjects();
            }
            OnPropertyChanged(nameof(Projects));
            OnPropertyChanged(nameof(MyProjects));
        });
        CloseProjectCommand = new RelayCommand(async o =>
        {
            if (o is Project project)
            {
                await _projectPageService.ChangeStatus(project.Id, Model.Enums.ProjectStatus.Closed);
                _projects = await _projectPageService.GetProjects();
            }
            OnPropertyChanged(nameof(MyProjects));
            OnPropertyChanged(nameof(Projects));
        });
        OpenProjectCommand = new RelayCommand (async o => 
        {
            if (o is Project project)
            {
                await _projectPageService.ChangeStatus(project.Id, Model.Enums.ProjectStatus.Opened);
                _projects = await _projectPageService.GetProjects();
            }
            OnPropertyChanged(nameof(MyProjects));
            OnPropertyChanged(nameof(Projects));
        });
        
        #endregion
        // Profile's functions //
        #region 
        ChangeLoginCommand = new RelayCommand(o => _profilePageService.OpenChangeLoginWindow());
        ChangePasswordCommand = new RelayCommand(o => _profilePageService.OpenChangePasswordWindow());
        ChangeEmailCommand = new RelayCommand(o => _profilePageService.OpenChangeEmailWindow());
        ChangeAboutTextCommand = new RelayCommand(o => _profilePageService.OpenChangeAboutTextWindow());

        LogOutCommand = new RelayCommand(o => _profilePageService.Logout(this));
        #endregion

        // Setting's functions //
        #region

        ChangeLocalizationCommand = new RelayCommand(o => _settingPageService.LocalizationService.Language = new CultureInfo((string)o));
        ApplySettingsСommand = new RelayCommand(o => _settingPageService.ApplySettings());

        #endregion
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}