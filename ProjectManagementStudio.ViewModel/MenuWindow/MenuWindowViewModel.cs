using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.Enums;
using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Comparers;
using ProjectManagementStudio.ViewModel.Pages;
using ProjectManagementStudio.ViewModel.PageServices.IProfilePageService;
using ProjectManagementStudio.ViewModel.PageServices.IProjectsPageService;
using ProjectManagementStudio.ViewModel.PageServices.ISettingsPageService;
using ProjectManagementStudio.ViewModel.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;

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
    private ObservableCollection<Sprint> _sprints = [];
    private ObservableCollection<User> _users = [];
    private ObservableCollection<SprintTask> _tasks = [];
    private ObservableCollection<Tag> _tags = [];
    private DateTime _selectedSprintDate = DateTime.Now;
    private readonly ISettingsPageService _settingPageService;
    private readonly IProjectsPageService _projectPageService;
    private User _selectedUser = new User();
    private SprintTask _newTask = new SprintTask();

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

    public ObservableCollection<Sprint> Sprints
    {
        get => _sprints;
        set
        {
            _sprints = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<User> Users
    {
        get => _users;
        set
        {
            _users = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<SprintTask> Tasks
    {
        get => _tasks;
        set
        {
            _tasks = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Project> MyProjects
    {
        get => [.. _projects.Where(x => x.HeadId)];
    }

    public ObservableCollection<Tag> Tags
    {
        get => _tags;
        set
        {
            _tags = value;
            OnPropertyChanged();
        }
    }

    public Project SelectedProject
    {
        get => _projectPageService.SelectedProject;
    }

    public Sprint SelectedSprint
    {
        get => _projectPageService.SelectedSprint;
        set
        {
            _projectPageService.SelectedSprint = value;
            OnPropertyChanged();
        }
    }

    public DateTime SelectedSprintDate
    {
        get => _selectedSprintDate;
        set
        {
            _selectedSprintDate = value;
            OnPropertyChanged();
        }
    }

    public User SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged();
        }
    }

    public SprintTask SelectedTask
    {
        get => _projectPageService.SelectedTask;
        set
        {
            _projectPageService.SelectedTask = value;
            if(SelectedTask is not null)
                _projectPageService.SelectedTask.Tags = [.. SelectedTask.Tags.Union(Tags, new TagComparer())];
            OnPropertyChanged();
        }
    }

    public SprintTask NewTask
    {
        get => _newTask;
        set
        {
            _newTask = value;
            OnPropertyChanged();
        }
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
    public ICommand NavigateToProjectDetailsPage { get; }
    public ICommand NavigateToSprintDetailsCommand { get; }

    public ICommand ChangeLoginCommand { get; }
    public ICommand ChangePasswordCommand { get; }
    public ICommand ChangeEmailCommand { get; }
    public ICommand ChangeAboutTextCommand { get; }

    public ICommand ChangeLocalizationCommand { get; }

    public ICommand LogOutCommand { get; }

    public ICommand LoadProjectsCommand { get; }
    public ICommand AddProjectCommand { get; }
    public ICommand StopProjectCommand { get; }
    public ICommand CloseProjectCommand { get; }
    public ICommand OpenProjectCommand { get; }

    public ICommand LoadSprintsCommand { get; }
    public ICommand LoadProjectUsersCommand { get; }
    public ICommand LoadSprintTasksCommand { get; }
    public ICommand LoadTagsCommand { get; }
    public ICommand CreateSprintCommand { get; }
    public ICommand CreateTaskCommand { get; }

    public ICommand SaveTaskCommand { get; }

    public ICommand ChangeApplicationTheme { get; }

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
        NavigateToProjectPage = new RelayCommand(o =>
        {
            ActivePage = _pageManager.NavigateTo(Pages.Pages.ProjectPage);
            _projectPageService.SelectedProject = null;
        });
        NavigateToProjectDetailsPage = new RelayCommand(o =>
        {
            ActivePage = _pageManager.NavigateTo(Pages.Pages.ProjectDetailsPage);
            if (SelectedProject is null)
            {
                _projectPageService.SelectedProject = o as Project ?? throw new NullReferenceException();
            }
        });
        NavigateToSprintDetailsCommand = new RelayCommand(o =>
        {
            ActivePage = _pageManager.NavigateTo(Pages.Pages.SprintDetailsPage);
            _projectPageService.SelectedSprint = o as Sprint ?? throw new NullReferenceException();
        });
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
        OpenProjectCommand = new RelayCommand(async o =>
        {
            if (o is Project project)
            {
                await _projectPageService.ChangeStatus(project.Id, Model.Enums.ProjectStatus.Opened);
                _projects = await _projectPageService.GetProjects();
            }
            OnPropertyChanged(nameof(MyProjects));
            OnPropertyChanged(nameof(Projects));
        });

        LoadSprintsCommand = new RelayCommand(async o => Sprints = await _projectPageService.GetSprints(_projectPageService.SelectedProject.Id));
        LoadProjectUsersCommand = new RelayCommand(async o => Users = await _projectPageService.GetProjectUsers(_projectPageService.SelectedProject.Id));
        LoadSprintTasksCommand = new RelayCommand(async o =>
        {
            Tasks = await _projectPageService.GetSprintTasks(_projectPageService.SelectedSprint.Id);
            OnPropertyChanged(nameof(Sprints));
            OnPropertyChanged(nameof(Tasks));
            OnPropertyChanged(nameof(Tags));
        });
        LoadTagsCommand = new RelayCommand(async o => Tags = await _projectPageService.GetTags());

        CreateSprintCommand = new RelayCommand(async o =>
        {
            if (SelectedSprintDate < DateTime.Now)
            {
                return;
            }

            var sprintDuration = SelectedSprintDate.Day - DateTime.Now.Day;

            await projectsPageService.CreateSprint(SelectedProject.Id, sprintDuration);
            Sprints = await _projectPageService.GetSprints(SelectedProject.Id);
            OnPropertyChanged(nameof(Sprints));
        });
        CreateTaskCommand = new RelayCommand(async o =>
        {
            await _projectPageService.CreateTask(SelectedSprint.Id, (int)_currentUserService.CurrentUser.UserId, new SprintTask()
            {
                TaskDescription = NewTask.TaskDescription,
                TaskName = NewTask.TaskName,
                Tags = [.. Tags.Where(x => x.IsChecked)]
            });

            Tasks = await _projectPageService.GetSprintTasks(SelectedSprint.Id);
        });
        SaveTaskCommand = new RelayCommand(async o =>
        {
            await _projectPageService.UpdateTask(SelectedTask);

            Tasks = await _projectPageService.GetSprintTasks(SelectedSprint.Id);
            OnPropertyChanged(nameof(Tasks));
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
        ChangeApplicationTheme = new RelayCommand(o => _settingPageService.ThemeService.SetTheme((Theme)o));
        #endregion
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}