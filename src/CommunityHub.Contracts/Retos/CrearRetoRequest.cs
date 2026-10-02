using System.ComponentModel.DataAnnotations;

namespace CommunityHub.Contracts.Retos;

// Clase con propiedades (y no record posicional) para que el mismo modelo
// sirva en la API y en un EditForm de Blazor, que necesita poder asignarlas.
public sealed class CrearRetoRequest
{
    [Required, StringLength(100)]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    public string Enunciado { get; set; } = string.Empty;

    [Range(1, 100)]
    public int Puntos { get; set; }
}
