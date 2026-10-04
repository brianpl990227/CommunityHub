using CommunityHub.Contracts.Retos;
using CommunityHub.Domain.Retos;

namespace CommunityHub.Application.Retos;

/// <summary>Un reto por su id, o <c>null</c> si no existe.</summary>
public sealed class ObtenerReto(IRetoRepository repositorio)
{
    public async Task<RetoResponse?> EjecutarAsync(int id, CancellationToken cancellationToken = default)
    {
        var reto = await repositorio.ObtenerPorIdAsync(id, cancellationToken);

        return reto?.ToResponse();
    }
}
