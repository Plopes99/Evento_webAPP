using Events_WebAPP.Client.Services;
using Events_WebAPP.Server;
using Microsoft.AspNetCore.Components;


namespace Events_WebAPP.Client.Pages;

public partial class EventDetails
{
    protected string Message = string.Empty;
    protected Event evento = new Event();
    
    [Parameter]
    public string Id { get; set; }
    [Inject]
    public SessionId session { get; set; }
    
    [Inject]
    public IEventService eventService { get; set; }
    
    [Inject]
    private NavigationManager navigationManagger { get; set; }

    protected async override Task OnInitializedAsync()
    {
        if (string.IsNullOrEmpty(Id))
        {
            //add
        }
        else
        {
            //update
            var eventId = Convert.ToInt32(Id);

            var apiEvent = await eventService.GetEvent(eventId);

            if (apiEvent != null)
                evento = apiEvent;

        }

    }

    private void GoToEvents()
    {
        navigationManagger.NavigateTo("/Events");
    }

    private void HandleFailedRequest()
    {
        Message = "Something went wrong, try again!";
    }

    protected async void HandleValidRequest()
    {
        if (string.IsNullOrEmpty(Id))
        {
            var result = await eventService.AddEvent(evento);
            navigationManagger.NavigateTo("/Event");
            
        }
        else
        {
            var result = await eventService.Update(evento);
            navigationManagger.NavigateTo("/Events");
        }
    }


    private async void DeleteEvent()
    {
        if (!string.IsNullOrEmpty(Id))
        {
            var eventId = Convert.ToInt32(Id);
            var result = await eventService.Delete(eventId);
            
            if(result)
                navigationManagger.NavigateTo("/Events");
            else
                Message = "Something went wrong!!";
        }
    }
}