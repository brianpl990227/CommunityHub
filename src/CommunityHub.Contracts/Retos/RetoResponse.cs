namespace CommunityHub.Contracts.Retos;

public sealed record RetoResponse(
    int Id,
    string Titulo,
    string Enunciado,
    int Puntos,
    DateTimeOffset PublicadoEl);
