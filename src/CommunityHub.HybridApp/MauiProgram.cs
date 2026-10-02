using CommunityHub.HybridApp.Services;
using CommunityHub.UI.Retos;
using CommunityHub.UI.Services;
using Microsoft.Extensions.Logging;

namespace CommunityHub.HybridApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Servicios que CommunityHub.UI necesita de cada host
        builder.Services.AddSingleton<IFormFactor, FormFactor>();

        // La app llama a la API directamente. El emulador de Android llega al localhost del PC
        // por 10.0.2.2 (https://learn.microsoft.com/dotnet/maui/data-cloud/local-web-services).
        var apiUrl = DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:5080/"
            : "http://localhost:5080/";
        builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(apiUrl) });
        builder.Services.AddScoped<IRetosClient, RetosClient>();

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
