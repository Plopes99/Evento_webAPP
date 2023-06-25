using Events_WebAPP.Client.Services;
using Events_WebAPP.Server;
using Microsoft.AspNetCore.Components;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;

namespace Events_WebAPP.Client.Pages;

public partial class Registrations
{
    protected string Message = string.Empty;
    protected Registration regist = new Registration();
    protected Event evento = new Event();
    
    [Parameter] 
    public String Id { get; set; }
    
    [Inject]
    public IRegistrationService registService { get; set; }
    
    [Inject]
    private IEventService eventService { get; set; }
    
    [Inject]
    private NavigationManager navigationManagger { get; set; }

    protected async override Task OnInitializedAsync()
    {
        //get event
        var eventId = Convert.ToInt32(Id);

        var apiEvent = await eventService.GetEvent(eventId);

        if (apiEvent != null)
        {
            evento = apiEvent;
            regist.Evt = evento;
            regist.Participant = SessionId.user;
            regist.EvtId = evento.EventId;
            regist.ParticipantId = SessionId.user.UserId;
        }
    }

    protected async void AddRegistration()
    {
        var result = await registService.AddRegistration(regist);
        navigationManagger.NavigateTo($"/RegistrationsDetails");

    }

    protected void GoToEvents()
    {
        navigationManagger.NavigateTo("/Events");
    }
    
    
    
    
}