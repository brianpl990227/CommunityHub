namespace CommunityHub.Domain.Retos;

/// <summary>
/// Un reto de código que se publica para la comunidad.
/// </summary>
public sealed class Reto
{
    public const int LongitudMaximaTitulo = 100;
    public const int PuntosMinimos = 1;
    public const int PuntosMaximos = 100;

    // Lo usa EF Core para materializar la entidad.
    private Reto() { }

    public int Id { get; private set; }
    public string Titulo { get; private set; } = string.Empty;
    public string Enunciado { get; private set; } = string.Empty;
    public int Puntos { get; private set; }
    public DateTimeOffset PublicadoEl { get; private set; }

    public static Reto Crear(string titulo, string enunciado, int puntos, DateTimeOffset publicadoEl)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(titulo);
        ArgumentException.ThrowIfNullOrWhiteSpace(enunciado);

        titulo = titulo.Trim();
        ArgumentOutOfRangeException.ThrowIfGreaterThan(titulo.Length, LongitudMaximaTitulo, nameof(titulo));
        ArgumentOutOfRangeException.ThrowIfLessThan(puntos, PuntosMinimos);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(puntos, PuntosMaximos);

        return new Reto
        {
            Titulo = titulo,
            Enunciado = enunciado.Trim(),
            Puntos = puntos,
            PublicadoEl = publicadoEl
        };
    }
}
