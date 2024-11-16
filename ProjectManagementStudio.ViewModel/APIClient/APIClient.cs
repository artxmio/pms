using Newtonsoft.Json;
using ProjectManagementStudio.Model.RequestsModels;
using ProjectManagementStudio.Model.ResponseModels;
using ProjectManagementStudio.Model.Responses;
using ProjectManagementStudio.Model.WindowModels.AuthModel;
using ProjectManagementStudio.Model.WindowModels.RegisterModel;
using System.Net.Http;
using System.Text;
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
        var json = JsonConvert.SerializeObject(new AuthRequestModel(user.login, user.password));
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var url = $"https://magpie-concrete-clearly.ngrok-free.app/is-user-exists/3i7r4ybfwbatro387";

        try
        {
            var response = await _client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            IsUserExistResponse deserializeResponse = JsonConvert.DeserializeObject<IsUserExistResponse>(await response.Content.ReadAsStringAsync())
                ?? throw new InvalidOperationException("Deserialized response can't be null");

            if (deserializeResponse is not null && deserializeResponse.Exists == "True")
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
        var json = JsonConvert.SerializeObject(new AddUserRequestModel(user.login, user.password, user.email));
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var url = $"https://magpie-concrete-clearly.ngrok-free.app/add-user/3i7r4ybfwbatro387";

        try
        {
            var response = await _client.PostAsync(url, content);

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

    private string MessagePerCode(int code)
    {
        string message = "успешно";
        switch (code)
        {
            case 2001: message = "Логин уже существует"; break;
            case 2002: message = "Email уже существует"; break;
        }

        return message;
    }
}