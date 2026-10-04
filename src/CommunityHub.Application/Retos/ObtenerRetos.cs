using CommunityHub.Contracts.Retos;
using CommunityHub.Domain.Retos;

namespace CommunityHub.Application.Retos;

/// <summary>Todos los retos, del más nuevo al más viejo.</summary>
public sealed class ObtenerRetos(IRetoRepository repositorio)
{
    public async Task<RetoResponse[]> EjecutarAsync(CancellationToken cancellationToken = default)
    {
        var retos = await repositorio.ObtenerTodosAsync(cancellationToken);

        return [.. retos.Select(r => r.ToResponse())];
    }
}
