using System.Text;
using System.Text.Json;
using Events_WebAPP.Server;

namespace Events_WebAPP.Client.Services;

public class UserService:IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<User>?> All()
    {
        try
        {
            var apiResponse = await _httpClient.GetStreamAsync("api/Users");

            var users = await JsonSerializer.DeserializeAsync<IEnumerable<User>>(apiResponse, new JsonSerializerOptions 
            {
                PropertyNameCaseInsensitive = true
            });

            return users;

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<User?> GetUser(int id)
    {
        try
        {
            var apiResponse = await _httpClient.GetStreamAsync($"api/Users/{id}");

            var user = await JsonSerializer.DeserializeAsync<User>(apiResponse, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }



    public async Task<User?> AddUser(User user)
    {
        try
        {
            var itemJson = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Users", itemJson);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStreamAsync();

                var addedUser = await JsonSerializer.DeserializeAsync<User>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return addedUser;
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<bool> Update(User user)
    {
        try
        {
            var itemJson = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Events/{user.UserId}", itemJson);

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Users/{id}");

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }
}