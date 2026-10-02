using CommunityHub.UI.Retos;
using CommunityHub.UI.Services;
using CommunityHub.Web.Components;
using CommunityHub.Web.Services;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Servicios que CommunityHub.UI necesita de cada host
builder.Services.AddSingleton<IFormFactor, FormFactor>();

// En el servidor (prerender e InteractiveServer) se llama a la API por service discovery.
builder.Services.AddHttpClient<IRetosClient, RetosClient>(client =>
    client.BaseAddress = new Uri("https+http://api"));

// Desde el navegador (WebAssembly) se llama a /api/ en este mismo dominio y la web lo reenvía
// a la API: el patrón "backend for frontend" que recomienda la documentación de Blazor.
builder.Services.AddHttpForwarderWithServiceDiscovery();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapDefaultEndpoints();

app.MapForwarder("/api/{**catch-all}", "https+http://api", transform => transform.AddPathRemovePrefix("/api"));

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(CommunityHub.UI._Imports).Assembly,
        typeof(CommunityHub.Web.Client._Imports).Assembly);

app.Run();
