using CommunityHub.Application.Retos;
using CommunityHub.Contracts.Retos;
using Microsoft.Extensions.Time.Testing;

namespace CommunityHub.Application.Tests;

public class RetosTests
{
    private readonly RetoRepositoryEnMemoria _repositorio = new();
    private readonly FakeTimeProvider _time = new(new DateTimeOffset(2026, 10, 5, 12, 0, 0, TimeSpan.Zero));

    [Fact]
    public async Task Crear_un_reto_lo_guarda_con_la_fecha_de_ahora()
    {
        var request = new CrearRetoRequest { Titulo = "  FizzBuzz  ", Enunciado = "Del 1 al 100.", Puntos = 10 };

        var creado = await new CrearReto(_repositorio, _time).EjecutarAsync(request);

        var guardado = Assert.Single(_repositorio.Retos);
        Assert.Equal("FizzBuzz", guardado.Titulo);
        Assert.Equal(_time.GetUtcNow(), creado.PublicadoEl);
    }

    [Fact]
    public async Task Un_reto_que_rompe_las_reglas_no_se_guarda()
    {
        var request = new CrearRetoRequest { Titulo = "FizzBuzz", Enunciado = "Del 1 al 100.", Puntos = 500 };

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            new CrearReto(_repositorio, _time).EjecutarAsync(request));

        Assert.Empty(_repositorio.Retos);
    }

    [Fact]
    public async Task Obtener_un_reto_que_no_existe_devuelve_null()
    {
        var reto = await new ObtenerReto(_repositorio).EjecutarAsync(99999);

        Assert.Null(reto);
    }
}
