using Events_WebAPP.Client.Services;
using Events_WebAPP.Server;
using Microsoft.AspNetCore.Components;

namespace Events_WebAPP.Client.Pages;

public partial class Activities
{
        
    protected string Message = string.Empty;
    protected Activity act = new Activity();

    [Parameter] 
    public String Id { get; set; }
    
    [Inject]
    public IActivityService acctivityService { get; set; }
    
    [Inject]
    private IEventService eventService { get; set; }
    
    [Inject]
    private NavigationManager navigationManager { get; set; }
    
    protected async override Task OnInitializedAsync()
    {
        var userId = Convert.ToInt32(Id);


    }
    
    protected async void HandleValidRequest()
    {
        if (Id != null)
        {
            int.TryParse(Id, out int parsedValue);
            act.EventId = parsedValue;
            
            TimeOnly time = new TimeOnly();
            act.Time = time;
            
            var result = await acctivityService.AddActivity(act);
            navigationManager.NavigateTo("/Events");
            
        }
        else
        {
            Message = "ERRO!";
        }
        
    }
    
    private void HandleFailedRequest()
    {
        Message = "Something went wrong, try again!";
    }
    
    private void GoToEvents()
    {
        navigationManager.NavigateTo("/Events");
    }
}
