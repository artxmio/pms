using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Model.MenuWindowModels.ProfileModel.ModalWindowModels.NewLogin;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Windows;
using System.Windows;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.NewEmail;

public class ChangeEmailModalWindowViewModel : IChangeEmailModalWindowViewModel
{
    private readonly IChangeLoginModel _model;
    private readonly IWindowManager _windowManager;
    private readonly IAPIClient _client;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserDataMementoWrapper _userDataMementoWrapper;

    public string NewEmail
    {
        get => _model.NewLogin;
        set => _model.NewLogin = value;
    }

    public string Password
    {
        get => _model.Password;
        set => _model.Password = value;
    }

    public ICommand CloseCommand { get; }
    public ICommand CangeEmailCommand { get; }

    public ChangeEmailModalWindowViewModel(
        IChangeLoginModel model,
        IWindowManager manager,
        IAPIClient client,
        ICurrentUserService currentUserService,
        IUserDataMementoWrapper userDataMementoWrapper)
    {
        _model = model;
        _windowManager = manager;
        _client = client;
        _currentUserService = currentUserService;
        _userDataMementoWrapper = userDataMementoWrapper;

        CloseCommand = new RelayCommand(o => CloseDialog());
        CangeEmailCommand = new AsyncCommand(ChangeEmail);
    }

    private async Task ChangeEmail()
    {
        //проверка на ввода текущего логина
        if (NewEmail.Equals(_currentUserService.CurrentUser.Email))
        {
            MessageBox.Show($"Почта должа отличаться от текущего!", "Ошибка!");
            return;
        }

        if (Password.Equals(_currentUserService.CurrentUser.Password))
        {
            // Отправляем запрос на изменение логина
            await _client.ChangeUserParametr(_currentUserService.CurrentUser, ChangeableParams.Email, NewEmail);
            MessageBox.Show($"Почта изменён на {NewEmail}", "Успех!");

            //обновляем локальные данные о пользователе
            _currentUserService.CurrentUser.Email = NewEmail;

            _windowManager.Close(this);
        }
        else
        {
            MessageBox.Show("Неверный пароль!", "Ошибка");
        }
    }

    private void CloseDialog()
    {
        // Обнуляем значения для свойств
        NewEmail = "";
        Password = "";

        // Закрываем окно
        _windowManager.Close(this);
    }
}
