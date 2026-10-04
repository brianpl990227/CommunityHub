using CommunityHub.Domain.Retos;

namespace CommunityHub.Application.Tests;

// Un repositorio de mentira: los casos de uso se prueban sin base de datos.
internal sealed class RetoRepositoryEnMemoria : IRetoRepository
{
    public List<Reto> Retos { get; } = [];

    public Task<IReadOnlyList<Reto>> ObtenerTodosAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<Reto>>([.. Retos.OrderByDescending(r => r.PublicadoEl)]);

    public Task<Reto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default) =>
        Task.FromResult(Retos.FirstOrDefault(r => r.Id == id));

    public Task AgregarAsync(Reto reto, CancellationToken cancellationToken = default)
    {
        Retos.Add(reto);
        return Task.CompletedTask;
    }
}
