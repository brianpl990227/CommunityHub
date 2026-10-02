using CommunityHub.UI.Retos;
using CommunityHub.UI.Services;
using CommunityHub.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Servicios que CommunityHub.UI necesita de cada host
builder.Services.AddSingleton<IFormFactor, FormFactor>();

// En el navegador se llama a /api/ del mismo dominio; la web lo reenvía a la API.
builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(new Uri(builder.HostEnvironment.BaseAddress), "api/")
});
builder.Services.AddScoped<IRetosClient, RetosClient>();

await builder.Build().RunAsync();
