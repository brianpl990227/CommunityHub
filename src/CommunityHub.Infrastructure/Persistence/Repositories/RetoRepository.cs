using CommunityHub.Domain.Retos;
using Microsoft.EntityFrameworkCore;

namespace CommunityHub.Infrastructure.Persistence.Repositories;

internal sealed class RetoRepository(CommunityHubDbContext db) : IRetoRepository
{
    public async Task<IReadOnlyList<Reto>> ObtenerTodosAsync(CancellationToken cancellationToken = default) =>
        await db.Retos
            .AsNoTracking()
            .OrderByDescending(r => r.PublicadoEl)
            .ToListAsync(cancellationToken);

    public Task<Reto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default) =>
        db.Retos.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task AgregarAsync(Reto reto, CancellationToken cancellationToken = default)
    {
        db.Retos.Add(reto);
        await db.SaveChangesAsync(cancellationToken);
    }
}
