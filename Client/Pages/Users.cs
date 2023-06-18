using Events_WebAPP.Client.Services;
using Microsoft.AspNetCore.Components;
using Events_WebAPP.Server;

namespace Events_WebAPP.Client.Pages;

public partial class Users
{
    [Inject]
    private IUserService userService {get; set;}

    public IEnumerable<User> _users { get; set; } = new List<User>();

    protected async override Task OnInitializedAsync()
    {
        var apiEvents = await userService.All();

        if (apiEvents != null && apiEvents.Any())
        {
            _users= apiEvents;
        }
    }
}