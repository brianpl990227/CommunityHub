using CommunityHub.Domain.Retos;
using CommunityHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommunityHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CommunityHubDbContext>(options => options
            .UseSqlite(connectionString)
            // Datos de ejemplo para que la app no arranque vacía
            // (https://learn.microsoft.com/ef/core/modeling/data-seeding#use-seeding-method).
            .UseSeeding((context, _) =>
            {
                var retos = context.Set<Reto>();
                if (!retos.Any())
                {
                    retos.AddRange(RetosDeEjemplo());
                    context.SaveChanges();
                }
            })
            .UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                var retos = context.Set<Reto>();
                if (!await retos.AnyAsync(cancellationToken))
                {
                    retos.AddRange(RetosDeEjemplo());
                    await context.SaveChangesAsync(cancellationToken);
                }
            }));

        return services;
    }

    private static Reto[] RetosDeEjemplo() =>
    [
        Reto.Crear(
            "FizzBuzz con pattern matching",
            "Del 1 al 100: Fizz para múltiplos de 3, Buzz para múltiplos de 5 y FizzBuzz para ambos. Sin un solo if.",
            10,
            new DateTimeOffset(2026, 10, 1, 12, 0, 0, TimeSpan.FromHours(-4))),
        Reto.Crear(
            "¿Dónde está el bug?",
            "Un endpoint de Minimal APIs responde 200 cuando el reto no existe. Encuentra el fallo y arréglalo.",
            20,
            new DateTimeOffset(2026, 10, 1, 12, 0, 0, TimeSpan.FromHours(-4)))
    ];
}
