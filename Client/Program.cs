using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Events_WebAPP.Client;
using Events_WebAPP.Client.Pages;
using Events_WebAPP.Client.Services;
using Microsoft.Extensions.DependencyInjection;
using Registrations_WebAPP.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<SessionId>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IActivityService, ActivityService>();

await builder.Build().RunAsync();
