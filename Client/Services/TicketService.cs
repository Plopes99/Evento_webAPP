using System.Text;
using System.Text.Json;
using Events_WebAPP.Server;

namespace Events_WebAPP.Client.Services;

public class TicketService : ITicketService
{
    private readonly HttpClient _httpClient;

    public TicketService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
    }
    
    public async Task<IEnumerable<Ticket>?> All()
    {
        try
        {
            var apiResponse = await _httpClient.GetStreamAsync("api/Tickets");

            var tickets = await JsonSerializer.DeserializeAsync<IEnumerable<Ticket>>(apiResponse, new JsonSerializerOptions 
            {
                PropertyNameCaseInsensitive = true
            });

            return tickets;

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<Ticket?> GetTicket(int id)
    {
        try
        {
            var apiResponse = await _httpClient.GetStreamAsync($"api/Tickets/{id}");

            var ticket = await JsonSerializer.DeserializeAsync<Ticket>(apiResponse, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return ticket;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<Ticket?> AddTicket(Ticket ticket)
    {
        try
        {
            var itemJson = new StringContent(JsonSerializer.Serialize(ticket), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Tickets", itemJson);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStreamAsync();

                var addedTicket = await JsonSerializer.DeserializeAsync<Ticket>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return addedTicket;
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }

    public async Task<bool> Update(Ticket ticket)
    {
        try
        {
            var itemJson = new StringContent(JsonSerializer.Serialize(ticket), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Tickets/{ticket.TicketId}", itemJson);

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
            var response = await _httpClient.DeleteAsync($"api/Tickets/{id}");

            return response.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            throw;
        }
    }
}