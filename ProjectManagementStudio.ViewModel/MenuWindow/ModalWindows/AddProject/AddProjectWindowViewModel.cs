using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.AddProject;

public class AddProjectWindowViewModel : IAddProjectWindowViewModel, INotifyPropertyChanged
{
    private readonly IWindowManager _windowManager;
    private readonly IAPIClient _apiClient;
    private readonly ICurrentUserService _currentUserService;
    private Project _newProject = new();

    public string Title
    {
        get => _newProject.Title;
        set
        {
            _newProject.Title = value;
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => _newProject.Description;
        set
        {
            _newProject.Description = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddProjectCommand { get; }
    public ICommand CloseWindowCommand { get; }

    public AddProjectWindowViewModel(
        IWindowManager windowManager,
        IAPIClient apiClient,
        ICurrentUserService currentUserService)
    {
        this._windowManager = windowManager;
        this._apiClient = apiClient;
        this._currentUserService = currentUserService;

        AddProjectCommand = new RelayCommand(async o => await AddProject());
        CloseWindowCommand = new RelayCommand(o => _windowManager.Close(this));
    }

    private async Task AddProject()
    {
        Regex titleRegex = new("^[a-zA-Z0-9\\s]{3,50}$");
        Regex descriptionRegex = new("^[a-zA-Z0-9.,!?\\s]{10,500}$");

        if (!titleRegex.IsMatch(_newProject.Title) || !descriptionRegex.IsMatch(_newProject.Description))
        {
            MessageBox.Show($"Введите все данные.", "Внимание");
            return;
        }

        await _apiClient.AddProject(_newProject, (int)_currentUserService.CurrentUser.UserId);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
