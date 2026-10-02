using CommunityHub.Contracts.Retos;
using CommunityHub.Domain.Retos;
using CommunityHub.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CommunityHub.Api.Retos;

public static class RetosEndpoints
{
    public static IEndpointRouteBuilder MapRetosEndpoints(this IEndpointRouteBuilder app)
    {
        var retos = app.MapGroup("/retos").WithTags("Retos");

        retos.MapGet("/", ObtenerTodos);
        retos.MapGet("/{id:int}", ObtenerPorId).WithName(nameof(ObtenerPorId));
        retos.MapPost("/", Crear);

        return app;
    }

    private static async Task<Ok<RetoResponse[]>> ObtenerTodos(
        CommunityHubDbContext db, CancellationToken cancellationToken)
    {
        var retos = await db.Retos
            .AsNoTracking()
            .OrderByDescending(r => r.PublicadoEl)
            .Select(r => new RetoResponse(r.Id, r.Titulo, r.Enunciado, r.Puntos, r.PublicadoEl))
            .ToArrayAsync(cancellationToken);

        return TypedResults.Ok(retos);
    }

    private static async Task<Results<Ok<RetoResponse>, NotFound>> ObtenerPorId(
        int id, CommunityHubDbContext db, CancellationToken cancellationToken)
    {
        var reto = await db.Retos.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        return reto is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(reto.ToResponse());
    }

    private static async Task<CreatedAtRoute<RetoResponse>> Crear(
        CrearRetoRequest request, CommunityHubDbContext db, TimeProvider time, CancellationToken cancellationToken)
    {
        var reto = Reto.Crear(request.Titulo, request.Enunciado, request.Puntos, time.GetUtcNow());

        db.Retos.Add(reto);
        await db.SaveChangesAsync(cancellationToken);

        return TypedResults.CreatedAtRoute(reto.ToResponse(), nameof(ObtenerPorId), new { id = reto.Id });
    }

    private static RetoResponse ToResponse(this Reto reto) =>
        new(reto.Id, reto.Titulo, reto.Enunciado, reto.Puntos, reto.PublicadoEl);
}
