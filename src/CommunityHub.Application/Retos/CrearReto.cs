using CommunityHub.Contracts.Retos;
using CommunityHub.Domain.Retos;

namespace CommunityHub.Application.Retos;

/// <summary>Publica un reto nuevo con la fecha de ahora.</summary>
public sealed class CrearReto(IRetoRepository repositorio, TimeProvider time)
{
    public async Task<RetoResponse> EjecutarAsync(CrearRetoRequest request, CancellationToken cancellationToken = default)
    {
        // Las reglas (título, rango de puntos) las comprueba el propio Reto.
        var reto = Reto.Crear(request.Titulo, request.Enunciado, request.Puntos, time.GetUtcNow());

        await repositorio.AgregarAsync(reto, cancellationToken);

        return reto.ToResponse();
    }
}
