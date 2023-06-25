using Events_WebAPP.Client.Services;
using Events_WebAPP.Server;
using Microsoft.AspNetCore.Components;

namespace Events_WebAPP.Client.Pages;

public partial class RegistrationDetails
{
    protected string Message = string.Empty;
    protected string Name = string.Empty;
    
    [Parameter]
    public string Id { get; set; }
    public IEnumerable<Registration> _regists { get; set; } = new List<Registration>();
    public IEnumerable<Event> _events { get; set; } = new List<Event>();
    
    [Inject]
    public IRegistrationService registService { get; set; }
    
    [Inject]
    public IEventService eventService { get; set; }
    
    private NavigationManager navigationManager { get; set; }
    

    protected async override Task OnInitializedAsync()
    {
        var apiRegistation = await registService.All();
        var apiEvents = await eventService.All();

        if (apiEvents != null && apiEvents.Any())
        {
            _events = apiEvents;
        }

        if (apiRegistation != null && apiEvents.Any())
        {
            _regists = apiRegistation;
        }
    }

    private void GoToLogin()
    {
        navigationManager.NavigateTo("/Login");
    }

}