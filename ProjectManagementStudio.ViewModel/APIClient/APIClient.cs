using ProjectManagementStudio.Model.AuthModel;
using ProjectManagementStudio.Model.Responses;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace ProjectManagementStudio.ViewModel.APIClient;

public class APIClient
{
    private readonly HttpClient _client = new();

    public APIClient()
    {
    }

    public async Task IsUserExists(AuthModel user)
    {
        //подготовка данных к запросу
        var json = JsonSerializer.Serialize(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var url = $"https://magpie-concrete-clearly.ngrok-free.app/is-user-exists/3i7r4ybfwbatro387";

        //получаем ответ от сервера
        var response = await _client.PostAsync(url, content);

        response.EnsureSuccessStatusCode();

        IsUserExistResponse? deserializeResponse = JsonSerializer.Deserialize<IsUserExistResponse>(await response.Content.ReadAsStringAsync());

        if (deserializeResponse is not null && deserializeResponse.exists == "True")
            MessageBox.Show("Есть такой пользователь");
        else
            MessageBox.Show("Нету такой пользователь");
    }
}