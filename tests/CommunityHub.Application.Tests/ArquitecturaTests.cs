using CommunityHub.Application.Retos;

namespace CommunityHub.Application.Tests;

public class ArquitecturaTests
{
    // Los casos de uso no saben de HTTP ni de base de datos.
    [Fact]
    public void La_capa_de_aplicacion_no_depende_de_aspnet_ni_de_entity_framework()
    {
        var referencias = typeof(CrearReto).Assembly.GetReferencedAssemblies().Select(a => a.Name!);

        Assert.DoesNotContain(referencias, r =>
            r.StartsWith("CommunityHub.Infrastructure", StringComparison.Ordinal) ||
            r.StartsWith("Microsoft.EntityFrameworkCore", StringComparison.Ordinal) ||
            r.StartsWith("Microsoft.AspNetCore", StringComparison.Ordinal));
    }
}
