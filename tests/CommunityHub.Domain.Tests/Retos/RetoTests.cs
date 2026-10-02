using CommunityHub.Domain.Retos;

namespace CommunityHub.Domain.Tests.Retos;

public class RetoTests
{
    private static readonly DateTimeOffset Ahora = new(2026, 10, 1, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Crear_con_datos_validos_devuelve_el_reto()
    {
        var reto = Reto.Crear("  FizzBuzz  ", "Del 1 al 100.", 10, Ahora);

        Assert.Equal("FizzBuzz", reto.Titulo);
        Assert.Equal("Del 1 al 100.", reto.Enunciado);
        Assert.Equal(10, reto.Puntos);
        Assert.Equal(Ahora, reto.PublicadoEl);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Crear_sin_titulo_lanza_excepcion(string titulo) =>
        Assert.Throws<ArgumentException>(() => Reto.Crear(titulo, "Enunciado", 10, Ahora));

    [Fact]
    public void Crear_con_titulo_demasiado_largo_lanza_excepcion() =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            Reto.Crear(new string('a', Reto.LongitudMaximaTitulo + 1), "Enunciado", 10, Ahora));

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    public void Crear_con_puntos_fuera_de_rango_lanza_excepcion(int puntos) =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Reto.Crear("FizzBuzz", "Enunciado", puntos, Ahora));
}
