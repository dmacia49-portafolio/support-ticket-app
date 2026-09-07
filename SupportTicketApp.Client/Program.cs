using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SupportTicketApp.Client;
using SupportTicketApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl =
    builder.Configuration["ApiBaseUrl"]
    ?? throw new InvalidOperationException("The API base URL was not configured.");

//Services
builder.Services.AddScoped<ITicketApiService, TicketApiService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });

await builder.Build().RunAsync();
