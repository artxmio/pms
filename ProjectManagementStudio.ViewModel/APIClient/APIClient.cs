using Newtonsoft.Json;
using ProjectManagementStudio.Model.RequestsModels;
using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.Model.Responses;
using ProjectManagementStudio.Model.WindowModels.AuthModel;
using ProjectManagementStudio.Model.WindowModels.RegisterModel;
using ProjectManagementStudio.ViewModel.UrlService;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace ProjectManagementStudio.ViewModel.APIClient;

public class APIClient : IAPIClient
{
    private readonly HttpClient _client = new();
    private readonly IUrlService _urlService;

    public APIClient(IUrlService urlService)
    {
        _urlService = urlService;

        _client.BaseAddress = new Uri($"{_urlService.URLBase}");
        _client.DefaultRequestHeaders.Add("Authorization", $"{_urlService.Token}");
    }

    public async Task<bool> IsUserExists(IAuthModel user)
    {
        var json = JsonConvert.SerializeObject(new AuthRequestModel(user.login, user.password));
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

    public async Task AddUser(IRegisterModel user)
    {
        var json = JsonConvert.SerializeObject(new AddUserRequestModel(user.login, user.password, user.email));
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

            deserializeResponse.Message = MessagePerCode(deserializeResponse.Code);

            if (deserializeResponse is not null && deserializeResponse.Success)
                MessageBox.Show("Успешно!");
            else
                MessageBox.Show($"Ошибка: {deserializeResponse?.Message} ({deserializeResponse?.Code})");
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

    private static string MessagePerCode(int code)
    {
        string message = "успешно";
        switch (code)
        {
            case 2001: message = "Логин уже существует"; break;
            case 2002: message = "Email уже существует"; break;
        }

        return message;
    }

    public async Task GetUserByLogin(IAuthModel currentUser)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_client.BaseAddress}get_user?login={currentUser.login}");

            var response = await _client.SendAsync(request);

            MessageBox.Show(await response.Content.ReadAsStringAsync());

            GetUserResponse deserializeResponse = JsonConvert.DeserializeObject<GetUserResponse>(await response.Content.ReadAsStringAsync()) 
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

    public async Task ChangeLogin(IAuthModel currentUser)
    {
        
    }
}