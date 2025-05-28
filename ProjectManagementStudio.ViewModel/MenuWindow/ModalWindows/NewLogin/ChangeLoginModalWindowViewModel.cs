using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Model.MenuWindowModels.ProfileModel.ModalWindowModels.NewLogin;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.APIClient.Enums;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Windows;
using System.Windows;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.NewLogin;

public class ChangeLoginModalWindowViewModel : IChangeLoginModalWindowViewModel
{
    private readonly IChangeLoginModel _model;
    private readonly IWindowManager _windowManager;
    private readonly IAPIClient _client;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserDataMementoWrapper _userDataMementoWrapper;

    public string NewLogin
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
    public ICommand CangeLoginCommand { get; }

    public ChangeLoginModalWindowViewModel(
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
        CangeLoginCommand = new AsyncCommand(ChangeLogin);
    }

    private async Task ChangeLogin()
    {
        //проверка на ввода текущего логина
        if (NewLogin.Equals(_currentUserService.CurrentUser.Login))
        {
            MessageBox.Show($"Логин должен отличаться от текущего!", "Ошибка!");
            return;
        }

        if (Password.Equals(_currentUserService.CurrentUser.Password))
        {
            // Отправляем запрос на изменение логина
            await _client.ChangeUserParametr(_currentUserService.CurrentUser.UserId, ChangeableParams.Login, NewLogin);
            MessageBox.Show($"Логин изменён на {NewLogin}", "Успех!");

            //обновляем локальные данные о пользователе
            _currentUserService.CurrentUser.Login = NewLogin;
            _userDataMementoWrapper.UserLogin = NewLogin;
            _userDataMementoWrapper.SaveUserData();

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
        NewLogin = "";
        Password = "";

        // Закрываем окно
        _windowManager.Close(this);
    }
}