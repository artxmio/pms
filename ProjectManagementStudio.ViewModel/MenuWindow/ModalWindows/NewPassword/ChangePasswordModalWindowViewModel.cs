using ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;
using ProjectManagementStudio.Model.MenuWindowModels.ProfileModel.ModalWindowModels.NewPassword;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.ViewModel.APIClient;
using ProjectManagementStudio.ViewModel.Command;
using ProjectManagementStudio.ViewModel.Windows;
using System.Windows;
using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.MenuWindow.ModalWindows.NewPassword;

public class ChangePasswordModalWindowViewModel : IChangePasswordModalWindowViewModel
{
    private readonly IChangePasswordModel _model;
    private readonly IWindowManager _windowManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly IAPIClient _client;
    private readonly IUserDataMementoWrapper _userDataMementoWrapper;

    public string OldPassword
    {
        get
        {
            return _model.OldPassword;
        }
        set
        {
            _model.OldPassword = value;
        }
    }
    public string NewPassword
    {
        get
        {
            return _model.NewPassword;
        }
        set
        {
            _model.NewPassword = value;
        }
    }

    public ICommand CloseCommand { get; }
    public ICommand ChangePasswordCommand { get; }

    public ChangePasswordModalWindowViewModel(
        IChangePasswordModel model, 
        IWindowManager manager, 
        ICurrentUserService currentUserService,
        IAPIClient client,
        IUserDataMementoWrapper userDataMementoWrapper)
    {
        _model = model;
        _windowManager = manager;
        _currentUserService = currentUserService;
        _client = client;
        _userDataMementoWrapper = userDataMementoWrapper;

        CloseCommand = new RelayCommand(o => CloseDialog());
        ChangePasswordCommand = new AsyncCommand(ChangePassword);
    }

    private async Task ChangePassword()
    {
        // Проверка на ввода текущего пароль
        if (NewPassword.Equals(_currentUserService.CurrentUser.Password))
        {
            MessageBox.Show($"Пароль должен отличаться от текущего!", "Ошибка!");
            return;
        }

        // Проверка на ввод текущего пароля
        if (!OldPassword.Equals(_currentUserService.CurrentUser.Password))
        {
            MessageBox.Show($"Неверный пароль!", "Ошибка!");
            return;
        }

        // Запрос на изменение пароля
        await _client.ChangeUserParametr(_currentUserService.CurrentUser, ChangeableParams.Password, NewPassword);

        // Обновление данных
        _userDataMementoWrapper.UserPassword = NewPassword;
        _userDataMementoWrapper.SaveUserData();
        _currentUserService.CurrentUser.Password = NewPassword;
        MessageBox.Show($"Пароль изменён на {NewPassword}", "Успех!");
        
        // Закрываем окно
        _windowManager.Close(this);
    }

    private void CloseDialog()
    {
        // Обнуляем значения
        OldPassword = "";
        NewPassword = "";

        // Закрываем окно
        _windowManager.Close(this);
    }
}
