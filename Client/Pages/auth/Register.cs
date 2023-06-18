using Events_WebAPP.Client.Services;
using Events_WebAPP.Server;
using Microsoft.AspNetCore.Components;

namespace Events_WebAPP.Client.Pages.auth;

public partial class Register
{
    protected string Message = string.Empty;

    protected User user { get; set; }= new User();
    
    [Parameter]
    public string Id { get; set; }
    
    [Inject]
    private IUserService userService { get; set; }
    
    [Inject]
    private NavigationManager navigationManager { get; set; }

    protected async override Task OnInitializedAsync()
    {
        if (string.IsNullOrEmpty(Id))
        {
            //ADD
        }
        else
        {
            var userId = Convert.ToInt32(Id);
            var apiUser = await userService.GetUser(userId);

            if (apiUser != null)
            {
                user = apiUser;
            }
        }
        
    }

    protected void HandleFailedRequest()
    {
        Message = "Something went wrong!";
    }

    protected void GoToLogin()
    {
        navigationManager.NavigateTo("Login");
    }

    protected async void HandleValidRequest()
    {

        var result = await userService.AddUser(user);
        navigationManager.NavigateTo("/Login");

    }
}