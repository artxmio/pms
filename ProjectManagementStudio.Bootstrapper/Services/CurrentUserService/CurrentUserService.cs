using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.PathService;
using ProjectManagementStudio.Model.WindowModels.AuthModel;

namespace ProjectManagementStudio.Bootstrapper.Services.CurrentUserService;

internal class CurrentUserService : ICurrentUserService, ICurrentUserServiceInitializer
{
    private ICurrentUserModel _currentUser;
    private bool _initialized;

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

    public CurrentUserService()
    {
        _currentUser = new CurrentUserModel();
    }

    public void Initialize()
    {
        if (_initialized)
            throw new InvalidOperationException($"{nameof(ICurrentUserService)} is already initialized");

        _initialized = true;
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
            throw new InvalidOperationException($"{nameof(ICurrentUserService)} is not initialized");
    }
}