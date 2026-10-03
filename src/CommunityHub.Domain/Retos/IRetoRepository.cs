namespace CommunityHub.Domain.Retos;

/// <summary>
/// Dónde se guardan los retos. La interfaz vive en el dominio y la implementa la infraestructura,
/// así quien la usa no sabe nada de la base de datos
/// (https://learn.microsoft.com/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture).
/// </summary>
public interface IRetoRepository
{
    /// <summary>Todos los retos, del más nuevo al más viejo.</summary>
    Task<IReadOnlyList<Reto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);

    Task<Reto?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);

    Task AgregarAsync(Reto reto, CancellationToken cancellationToken = default);
}
