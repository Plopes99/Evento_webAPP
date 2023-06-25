using System.Text;
using System.Text.Json;
using Events_WebAPP.Client.Services;
using Events_WebAPP.Server;

namespace Registrations_WebAPP.Client.Services;

public class RegistrationService :IRegistrationService
{
    private readonly HttpClient _httpClient;

    public RegistrationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<Registration>?> All()
    {
        try
        {
            var apiResponse = await _httpClient.GetStreamAsync("api/Registrations");

            var regist = await JsonSerializer.DeserializeAsync<IEnumerable<Registration>>(apiResponse, new JsonSerializerOptions 
            {
                PropertyNameCaseInsensitive = true
            });

            return regist;

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<Registration?> GetRegistration(int id)
    {
        try
        {
            var apiResponse = await _httpClient.GetStreamAsync($"api/Registrations/{id}");

            var regist = await JsonSerializer.DeserializeAsync<Registration>(apiResponse, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return regist;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<Registration?> AddRegistration(Registration regist)
    {
        try
        {
            var itemJson = new StringContent(JsonSerializer.Serialize(regist), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Registrations", itemJson);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStreamAsync();

                var addedRegistration = await JsonSerializer.DeserializeAsync<Registration>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return addedRegistration;
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<bool> Update(Registration regist)
    {
        try
        {
            var itemJson = new StringContent(JsonSerializer.Serialize(regist), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Registrations/{regist.RegistrationId}", itemJson);

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
            var response = await _httpClient.DeleteAsync($"api/Registrations/{id}");

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }
}