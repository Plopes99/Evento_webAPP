using System.Text;
using System.Text.Json;
using Events_WebAPP.Server;

namespace Events_WebAPP.Client.Services;

public class ActivityService: IActivityService
{
    private readonly HttpClient _httpClient;

    public ActivityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
    }
    
    public async Task<IEnumerable<Activity>?> All()
    {
        try
        {
            var apiResponse = await _httpClient.GetStreamAsync("api/Activities");

            var activities = await JsonSerializer.DeserializeAsync<IEnumerable<Activity>>(apiResponse, new JsonSerializerOptions 
            {
                PropertyNameCaseInsensitive = true
            });

            return activities;

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<Activity?> GetActivity(int id)
    {
        try
        {
            var apiResponse = await _httpClient.GetStreamAsync($"api/Activities/{id}");

            var activity = await JsonSerializer.DeserializeAsync<Activity>(apiResponse, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return activity;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<Activity?> AddActivity(Activity activity)
    {
        try
        {
            var itemJson = new StringContent(JsonSerializer.Serialize(activity), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Activities", itemJson);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStreamAsync();

                var addedActivity = await JsonSerializer.DeserializeAsync<Activity>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return addedActivity;
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<bool> Update(Activity activity)
    {
        try
        {
            var itemJson = new StringContent(JsonSerializer.Serialize(activity), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Activities/{activity.ActivityId}", itemJson);

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
            var response = await _httpClient.DeleteAsync($"api/Activities/{id}");

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }
    
}