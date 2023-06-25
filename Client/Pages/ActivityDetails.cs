using Events_WebAPP.Client.Services;
using Events_WebAPP.Server;
using Microsoft.AspNetCore.Components;

namespace Events_WebAPP.Client.Pages;

public partial class ActivityDetails
{
    protected string Message = string.Empty;
    protected string Name = string.Empty;
    
    [Parameter]
    public string Id { get; set; }
    public IEnumerable<Activity> _activities{ get; set; } = new List<Activity>();
    
    [Inject]
    public IActivityService activityService { get; set; }
    
    [Inject]
    private NavigationManager navigationManager { get; set; }
    

    protected async override Task OnInitializedAsync()
    {
        var apiActivities = await activityService.All();

        if (apiActivities != null && apiActivities.Any())
        {
            _activities = apiActivities;
        }
        
    }
    
    private void GoToEvents()
    {
        navigationManager.NavigateTo("/Events");
    }
}