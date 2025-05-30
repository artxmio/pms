using ProjectManagementStudio.ViewModel.Windows;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.MenuWindow;

public interface IMenuWindowViewModel : IWindowViewModel
{
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
    public ICommand ApplySettingsСommand { get; }

    public ICommand LogOutCommand { get; }

    public ICommand LoadProjectsCommand { get; }
    public ICommand AddProjectCommand { get; }
    public ICommand StopProjectCommand { get; }
    public ICommand CloseProjectCommand { get; }
    public ICommand OpenProjectCommand { get; }

    public ICommand LoadSprintsCommand { get; }
    public ICommand LoadProjectUsersCommand { get; }
    public ICommand LoadSprintTasksCommand { get; }
}