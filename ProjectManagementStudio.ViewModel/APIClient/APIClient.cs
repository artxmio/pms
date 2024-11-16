using ProjectManagementStudio.Model.RequestsModels;
using ProjectManagementStudio.Model.Responses;
using ProjectManagementStudio.Model.WindowModels.AuthModel;
using ProjectManagementStudio.Model.WindowModels.RegisterModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace ProjectManagementStudio.ViewModel.APIClient;

public class APIClient : IAPIClient
{
    private readonly HttpClient _client = new();

    public APIClient()
    {
    }

    public async Task IsUserExists(AuthModel user)
    {
        var json = JsonSerializer.Serialize(new AuthRequestModel(user.login, user.password));
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var url = $"https://magpie-concrete-clearly.ngrok-free.app/is-user-exists/3i7r4ybfwbatro387";

        try
        {
            var response = await _client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            IsUserExistResponse? deserializeResponse = JsonSerializer.Deserialize<IsUserExistResponse>(await response.Content.ReadAsStringAsync());

            if (deserializeResponse is not null && deserializeResponse.exists == "True")
                MessageBox.Show("Есть такой пользователь");
            else
                MessageBox.Show("Нету такой пользователь");
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show($"Возникла ошибка: сервер отключён или недоступен ({ex.Message})", "Ошибка");
        }
        catch(Exception ex)
        {
            MessageBox.Show($"Возникла неизвестная ошибка: {ex.Message}", "Ошибка");
        }
    }

    public async Task AddUser(RegisterModel user) 
    {
        var json = JsonSerializer.Serialize(new AddUserRequestModel(user.login, user.password, user.email));
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var url = $"https://magpie-concrete-clearly.ngrok-free.app/add-user/3i7r4ybfwbatro387";

        try
        {
            var response = await _client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();
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
}