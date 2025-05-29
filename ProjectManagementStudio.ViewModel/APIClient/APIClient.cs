using Newtonsoft.Json;
using ProjectManagementStudio.Model.CurrentUserModel;
using ProjectManagementStudio.Model.Enums;
using ProjectManagementStudio.Model.RequestsModels;
using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.Model.Responses;
using ProjectManagementStudio.ViewModel.APIClient.Enums;
using ProjectManagementStudio.ViewModel.UrlService;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace ProjectManagementStudio.ViewModel.APIClient;

public class APIClient : IAPIClient
{
    private readonly HttpClient _client = new();
    private readonly IUrlService _urlService;

    private readonly Dictionary<ChangeableParams, string> _paramNames = new()
    {
        { ChangeableParams.Login, "login" },
        { ChangeableParams.Password, "password" },
        { ChangeableParams.Email, "email" },
        { ChangeableParams.AboutText, "aboute" }
    };

    public APIClient(IUrlService urlService)
    {
        _urlService = urlService;

        _client.BaseAddress = new Uri($"{_urlService.URLBase}");
        _client.DefaultRequestHeaders.Add("Authorization", $"{_urlService.Token}");
    }

    public async Task<bool> IsUserExists(string login, string password)
    {
        var json = JsonConvert.SerializeObject(new AuthRequestModel(login, password));
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _urlService.URLEndpoint = nameof(IsUserExists);

        bool isExist = false;

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_client.BaseAddress}{_urlService.URLEndpoint}")
            {
                Content = content
            };

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            IsUserExistResponse deserializeResponse = JsonConvert.DeserializeObject<IsUserExistResponse>(await response.Content.ReadAsStringAsync())
                ?? throw new InvalidOperationException("Deserialized response can't be null");

            isExist = deserializeResponse is not null && deserializeResponse.Success;
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Возникла ошибка: сервер отключён или недоступен ({ex.Message})", "Ошибка");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Возникла неизвестная ошибка: {ex.Message}", "Ошибка");
        }

        return isExist;
    }

    public async Task<bool> AddUser(string login, string password, string email)
    {
        var json = JsonConvert.SerializeObject(new AddUserRequestModel(login, password, email));
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _urlService.URLEndpoint = nameof(AddUser);

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_client.BaseAddress}{_urlService.URLEndpoint}")
            {
                Content = content
            };

            var response = await _client.SendAsync(request);

            AddUserResponse deserializeResponse = JsonConvert.DeserializeObject<AddUserResponse>(await response.Content.ReadAsStringAsync())
                ?? throw new InvalidOperationException("Deserialized response can't be null");

            if (deserializeResponse is not null && deserializeResponse.Success)
            {
                MessageBox.Show("Успешно!");
                return true;
            }
            else
            {
                MessageBox.Show($"{deserializeResponse?.Message}");
                return false;
            }
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Возникла ошибка: сервер отключён или недоступен ({ex.Message})", "Ошибка");
            return false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Возникла неизвестная ошибка: {ex.Message}", "Ошибка");
            return false;
        }
    }

    public async Task<ICurrentUserModel> GetUserByLogin(string login, string password)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_client.BaseAddress}/api-v2/user/get?login={login}");

            var response = await _client.SendAsync(request);

            GetUserResponse deserializeResponse = JsonConvert.DeserializeObject<GetUserResponse>(await response.Content.ReadAsStringAsync())
                ?? throw new InvalidOperationException("Deserialized response can't be null");

            ICurrentUserModel user = new CurrentUserModel()
            {
                UserId = (long)deserializeResponse.Data["id"],
                Login = (string)deserializeResponse.Data["login"],
                Password = password,
                Email = (string)deserializeResponse.Data["email"],
            };

            return user;
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Возникла ошибка: сервер отключён или недоступен ({ex.Message})", "Ошибка");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        throw new InvalidOperationException();
    }

    public async Task ChangeUserParametr(long id, ChangeableParams parametr, string newValue)
    {
        var json = JsonConvert.SerializeObject(new ChangeUserParamsRequestModel(id, _paramNames[parametr], newValue));
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _urlService.URLEndpoint = nameof(ChangeUserParametr);

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_client.BaseAddress}{_urlService.URLEndpoint}")
            {
                Content = content
            };

            var response = await _client.SendAsync(request);

            ChangeUserParamsResponseModel deserializeResponse = JsonConvert.DeserializeObject<ChangeUserParamsResponseModel>(await response.Content.ReadAsStringAsync())
                ?? throw new InvalidOperationException("Deserialized response can't be null");
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

    public async Task<ObservableCollection<Project>> GetProjects(int userId)
    {
        _urlService.URLEndpoint = nameof(GetProjects);

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_client.BaseAddress}/{_urlService.URLEndpoint}?user_id={userId}");

            var response = await _client.SendAsync(request);

            Debug.WriteLine(response.StatusCode);
            Debug.WriteLine(response.RequestMessage);

            var deserealizedList = JsonConvert.DeserializeObject<ProjectResponse>(await response.Content.ReadAsStringAsync())
                ?? throw new InvalidOperationException("Deserialized response can't be null");

            return [.. deserealizedList.Data];
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Возникла ошибка: сервер отключён или недоступен ({ex.Message})", "Ошибка");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        throw new InvalidOperationException();
    }

    public async Task AddProject(Project project, int userId)
    {
        _urlService.URLEndpoint = nameof(AddProject);

        var serializableProject = new AddProjectRequestModel
        {
            ProjectDescription = project.Description,
            ProjectTitle = project.Title,
            UserId = userId
        };

        var json = JsonConvert.SerializeObject(serializableProject);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_client.BaseAddress}{_urlService.URLEndpoint}")
            {
                Content = content
            };
            var response = await _client.SendAsync(request);

            AddProjectResponse deserializeResponse = JsonConvert.DeserializeObject<AddProjectResponse>(await response.Content.ReadAsStringAsync())
                ?? throw new InvalidOperationException("Deserialized response can't be null");
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Возникла ошибка: сервер отключён или недоступен ({ex.Message})", "Ошибка");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Возникла неизвестная ошибка: {ex.Message}", "Ошибка");
        }
    }

    public async Task ChangeProjectStatus(int projectId, ProjectStatus status)
    {
        _urlService.URLEndpoint = nameof(ChangeProjectStatus);

        try
        {
            var json = JsonConvert.SerializeObject(new ChangeProjectStatusRequestModel(projectId, status));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_client.BaseAddress}{_urlService.URLEndpoint}")
            {
                Content = content
            };

            var response = await _client.SendAsync(request);

            ChangeProjectStatusResponse deserializeResponse = JsonConvert.DeserializeObject<ChangeProjectStatusResponse>(await response.Content.ReadAsStringAsync())
                ?? throw new InvalidOperationException("Deserialized response can't be null"); ;
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

    public async Task<ObservableCollection<Sprint>> GetSprintsByProjectID(int projectId)
    {
        _urlService.URLEndpoint = nameof(GetSprintsByProjectID);
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_client.BaseAddress}{_urlService.URLEndpoint}?project_id={projectId}");

            var response = await _client.SendAsync(request);

            var deserializeResponse = JsonConvert.DeserializeObject<GetSprintsResponse>(await response.Content.ReadAsStringAsync()) 
                ?? throw new InvalidOperationException("Deserialized response can't be null"); ;

            return [.. deserializeResponse.Data];
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Возникла ошибка: сервер отключён или недоступен ({ex.Message})", "Ошибка");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        throw new InvalidOperationException();
    }
}