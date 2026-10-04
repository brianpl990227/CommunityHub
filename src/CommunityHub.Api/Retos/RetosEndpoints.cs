using CommunityHub.Application.Retos;
using CommunityHub.Contracts.Retos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CommunityHub.Api.Retos;

// Los endpoints solo traducen HTTP: reciben la petición, llaman al caso de uso y eligen la respuesta.
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
        ObtenerRetos obtenerRetos, CancellationToken cancellationToken) =>
        TypedResults.Ok(await obtenerRetos.EjecutarAsync(cancellationToken));

    private static async Task<Results<Ok<RetoResponse>, NotFound>> ObtenerPorId(
        int id, ObtenerReto obtenerReto, CancellationToken cancellationToken)
    {
        var reto = await obtenerReto.EjecutarAsync(id, cancellationToken);

        return reto is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(reto);
    }

    private static async Task<CreatedAtRoute<RetoResponse>> Crear(
        CrearRetoRequest request, CrearReto crearReto, CancellationToken cancellationToken)
    {
        var reto = await crearReto.EjecutarAsync(request, cancellationToken);

        return TypedResults.CreatedAtRoute(reto, nameof(ObtenerPorId), new { id = reto.Id });
    }
}
