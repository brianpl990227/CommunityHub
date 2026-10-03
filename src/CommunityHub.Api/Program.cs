using CommunityHub.Api.Retos;
using CommunityHub.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("communityhub")
    ?? throw new InvalidOperationException("Falta la cadena de conexión 'communityhub'."));

builder.Services.AddProblemDetails();
builder.Services.AddValidation();
builder.Services.AddOpenApi();
builder.Services.AddSingleton(TimeProvider.System);

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // En desarrollo, la base de datos se crea y se rellena sola al arrancar.
    await app.Services.MigrarBaseDeDatosAsync();
}

app.MapDefaultEndpoints();
app.MapRetosEndpoints();

app.Run();
