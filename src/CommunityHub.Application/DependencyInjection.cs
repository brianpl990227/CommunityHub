using CommunityHub.Application.Retos;
using Microsoft.Extensions.DependencyInjection;

namespace CommunityHub.Application;

/// <summary>
/// Los casos de uso de la app. Coordinan el dominio y los repositorios, pero las reglas del negocio
/// siguen en las entidades
/// (https://learn.microsoft.com/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice#layers-in-ddd-microservices).
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ObtenerRetos>();
        services.AddScoped<ObtenerReto>();
        services.AddScoped<CrearReto>();

        return services;
    }
}
