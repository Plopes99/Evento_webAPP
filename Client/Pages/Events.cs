using Events_WebAPP.Client.Services;
using Events_WebAPP.Server;
using Microsoft.AspNetCore.Components;

namespace Events_WebAPP.Client.Pages;

public partial class Events
{
    [Inject]
    private IEventService eventService {get; set;}

    public IEnumerable<Event> _envents { get; set; } = new List<Event>();
    
    private NavigationManager navigationManager { get; set; }

    protected async override Task OnInitializedAsync()
    {
        var apiEvents = await eventService.All();

        if (apiEvents != null && apiEvents.Any())
        {
            _envents = apiEvents;
        }
    }
    
    private void GoToLogin()
    {
        navigationManager.NavigateTo("/Login");
    }
}