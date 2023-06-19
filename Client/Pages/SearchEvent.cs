using Events_WebAPP.Client.Services;
using Events_WebAPP.Server;
using Microsoft.AspNetCore.Components;

namespace Events_WebAPP.Client.Pages;

public partial class SearchEvent
{
    protected string Message = string.Empty;
    protected Event evento = new Event();
    
    [Parameter]
    public string Id { get; set; }
    
    [Inject]
    public IEventService eventService { get; set; }
    public IEnumerable<Event> _envents { get; set; } = new List<Event>();
    
    [Inject]
    private NavigationManager navigationManagger { get; set; }

    protected async override Task OnInitializedAsync()
    {
        var apiEvents = await eventService.All();

        if (apiEvents != null && apiEvents.Any())
        {
            _envents = apiEvents;
        }

    }
    
    private void HandleFailedRequest()
    {
        Message = "Something went wrong, try again!";
    }
    
    private void GoToHome()
    {
        navigationManagger.NavigateTo("/Events");
    }
    
    protected async void HandleValidRequest()
    {
        foreach (var i in _envents)
        {
            
        }
    }
}