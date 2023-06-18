using System.Text;
using System.Text.Json;
using Events_WebAPP.Server;

namespace Events_WebAPP.Client.Services;

public class EventService:IEventService
{
    private readonly HttpClient _httpClient;

    public EventService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<Event>?> All()
    {
        try
        {
            var apiResponse = await _httpClient.GetStreamAsync("api/Events");

            var eventos = await JsonSerializer.DeserializeAsync<IEnumerable<Event>>(apiResponse, new JsonSerializerOptions 
            {
                PropertyNameCaseInsensitive = true
            });

            return eventos;

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<Event?> GetEvent(int id)
    {
        try
        {
            var apiResponse = await _httpClient.GetStreamAsync($"api/Events/{id}");

            var evento = await JsonSerializer.DeserializeAsync<Event>(apiResponse, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return evento;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<Event?> AddEvent(Event evento)
    {
        try
        {
            var itemJson = new StringContent(JsonSerializer.Serialize(evento), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Events", itemJson);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStreamAsync();

                var addedEvent = await JsonSerializer.DeserializeAsync<Event>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return addedEvent;
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<bool> Update(Event evento)
    {
        try
        {
            var itemJson = new StringContent(JsonSerializer.Serialize(evento), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Events/{evento.EventId}", itemJson);

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
            var response = await _httpClient.DeleteAsync($"api/Events/{id}");

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }
}