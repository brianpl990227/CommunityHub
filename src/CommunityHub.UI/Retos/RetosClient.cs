using System.Net.Http.Json;
using CommunityHub.Contracts.Retos;

namespace CommunityHub.UI.Retos;

public sealed class RetosClient(HttpClient http) : IRetosClient
{
    public async Task<RetoResponse[]> ObtenerTodosAsync(CancellationToken cancellationToken = default) =>
        await http.GetFromJsonAsync<RetoResponse[]>("retos", cancellationToken) ?? [];
}
