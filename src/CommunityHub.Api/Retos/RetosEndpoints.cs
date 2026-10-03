using CommunityHub.Contracts.Retos;
using CommunityHub.Domain.Retos;
using Microsoft.AspNetCore.Http.HttpResults;

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
        IRetoRepository repositorio, CancellationToken cancellationToken)
    {
        var retos = await repositorio.ObtenerTodosAsync(cancellationToken);

        return TypedResults.Ok(retos.Select(r => r.ToResponse()).ToArray());
    }

    private static async Task<Results<Ok<RetoResponse>, NotFound>> ObtenerPorId(
        int id, IRetoRepository repositorio, CancellationToken cancellationToken)
    {
        var reto = await repositorio.ObtenerPorIdAsync(id, cancellationToken);

        return reto is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(reto.ToResponse());
    }

    private static async Task<CreatedAtRoute<RetoResponse>> Crear(
        CrearRetoRequest request, IRetoRepository repositorio, TimeProvider time, CancellationToken cancellationToken)
    {
        var reto = Reto.Crear(request.Titulo, request.Enunciado, request.Puntos, time.GetUtcNow());

        await repositorio.AgregarAsync(reto, cancellationToken);

        return TypedResults.CreatedAtRoute(reto.ToResponse(), nameof(ObtenerPorId), new { id = reto.Id });
    }

    private static RetoResponse ToResponse(this Reto reto) =>
        new(reto.Id, reto.Titulo, reto.Enunciado, reto.Puntos, reto.PublicadoEl);
}
