using Events_WebAPP.Client.Services;
using Microsoft.AspNetCore.Components;
using Events_WebAPP.Server;

namespace Events_WebAPP.Client.Pages.auth;

public partial class Login
{
    [Inject]
    private IUserService userService {get; set;}
    private NavigationManager navigationManager { get; set; }

    public IEnumerable<User> _users { get; set; } = new List<User>();

    protected async override Task OnInitializedAsync()
    {
        var apiEvents = await userService.All();

        if (apiEvents != null && apiEvents.Any())
        {
            _users= apiEvents;
        }
    }

    protected void goToHome()
    {
        navigationManager.NavigateTo("/Home");
    }

   
}