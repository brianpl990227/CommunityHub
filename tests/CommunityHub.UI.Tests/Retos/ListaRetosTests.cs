using Bunit;
using CommunityHub.Contracts.Retos;
using CommunityHub.UI.Pages.Retos;
using CommunityHub.UI.Retos;
using Microsoft.Extensions.DependencyInjection;

namespace CommunityHub.UI.Tests.Retos;

public class ListaRetosTests : BunitContext
{
    [Fact]
    public void Muestra_un_elemento_por_cada_reto()
    {
        Services.AddSingleton<IRetosClient>(new RetosClientFalso(
            new RetoResponse(1, "FizzBuzz", "Del 1 al 100.", 10, DateTimeOffset.UnixEpoch),
            new RetoResponse(2, "¿Dónde está el bug?", "Un 200 que debería ser 404.", 20, DateTimeOffset.UnixEpoch)));

        var cut = Render<ListaRetos>();

        cut.WaitForAssertion(() => Assert.Equal(2, cut.FindAll("li.reto").Count));
        Assert.Contains("FizzBuzz", cut.Markup);
    }

    [Fact]
    public void Sin_retos_muestra_un_mensaje()
    {
        Services.AddSingleton<IRetosClient>(new RetosClientFalso());

        var cut = Render<ListaRetos>();

        cut.WaitForAssertion(() => Assert.Contains("Todavía no hay retos", cut.Markup));
    }

    private sealed class RetosClientFalso(params RetoResponse[] retos) : IRetosClient
    {
        public Task<RetoResponse[]> ObtenerTodosAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult(retos);
    }
}
