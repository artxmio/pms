using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.UserSavedData.Wrapper;
using ProjectManagementStudio.Model.WindowModels.AuthModel;
using ProjectManagementStudio.ViewModel.APIClient;
using System.Net.Http;
using System.Windows;

namespace ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;

internal class CurrentUserService : ICurrentUserService, ICurrentUserServiceInitializer
{
    private ICurrentUserModel _currentUser;
    private bool _initialized;

    private readonly IAPIClient _apiClient;
    private readonly IUserDataMementoWrapper _userDataMementoWrapper;

    public ICurrentUserModel CurrentUser
    {
        get
        {
            EnsureInitialized();
            return _currentUser;
        }

        set
        {
            EnsureInitialized();
            _currentUser = value;
        }
    }

    public CurrentUserService(IAPIClient client, IUserDataMementoWrapper userDataMementoWrapper, ICurrentUserModel currentUserModel)
    {
        _currentUser = currentUserModel;
        _apiClient = client;
        _userDataMementoWrapper = userDataMementoWrapper;
    }

    public async void Initialize()
    {
        if (_initialized)
            throw new InvalidOperationException($"{nameof(ICurrentUserService)} is already initialized");

        _initialized = true;

        try
        {
            if (!_userDataMementoWrapper.IsRememberMe)
            {
                return;
            }

            if (!_userDataMementoWrapper.IsFileNull)
            {
                var authModel = new AuthModel(_userDataMementoWrapper);
                CurrentUser = await _apiClient.GetUserByLogin(authModel);
                CurrentUser.AboutText = _userDataMementoWrapper.AboutText;
            }
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Возникла ошибка: сервер отключён или недоступен ({ex.Message})", "Ошибка");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"{nameof(ICurrentUserService)} is not initialized");
    }
}