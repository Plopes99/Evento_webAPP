using Events_WebAPP.Client.Services;
using Events_WebAPP.Server;
using Microsoft.AspNetCore.Components;

namespace Events_WebAPP.Client.Pages;

public partial class EditUser
{
    [Inject] private IUserService userService { get; set; }
    protected string Message = string.Empty;

    [Parameter] public string Id { get; set; }

    protected User user = new User();

    private NavigationManager navigationManagger { get; set; }

    protected async override Task OnInitializedAsync()
    {
        var userId = Convert.ToInt32(Id);

        var apiUser = await userService.GetUser(userId);

        if (apiUser != null)
            user = apiUser;
    }

    private void GoToHome()
    {
        navigationManagger.NavigateTo(" ");
    }

    private void HandleFailedRequest()
    {
        Message = "Something went wrong, try again!";
    }

    protected async void HandleValidRequest()
    {
        var result = await userService.Update(user);
        Message = "SUCCESS!!";
    }
}