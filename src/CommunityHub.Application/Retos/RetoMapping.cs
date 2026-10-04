using CommunityHub.Contracts.Retos;
using CommunityHub.Domain.Retos;

namespace CommunityHub.Application.Retos;

internal static class RetoMapping
{
    public static RetoResponse ToResponse(this Reto reto) =>
        new(reto.Id, reto.Titulo, reto.Enunciado, reto.Puntos, reto.PublicadoEl);
}
