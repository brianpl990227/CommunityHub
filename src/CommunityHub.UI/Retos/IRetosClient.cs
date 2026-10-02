using CommunityHub.Contracts.Retos;

namespace CommunityHub.UI.Retos;

/// <summary>
/// Acceso a los retos desde la interfaz. Cada host (servidor, WebAssembly y la app)
/// registra el HttpClient que le toca; los componentes no saben dónde corren.
/// </summary>
public interface IRetosClient
{
    Task<RetoResponse[]> ObtenerTodosAsync(CancellationToken cancellationToken = default);
}
