using CommunityHub.Contracts.Retos;
using CommunityHub.Domain.Retos;

namespace CommunityHub.Application.Retos;

internal static class RetoMapping
{
    extension(Reto reto)
    {
        public RetoResponse ToResponse() =>
            new(reto.Id, reto.Titulo, reto.Enunciado, reto.Puntos, reto.PublicadoEl);
    }
}
