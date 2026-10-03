using CommunityHub.Domain.Retos;

namespace CommunityHub.Domain.Tests;

public class ArquitecturaTests
{
    // La regla de dependencias: el dominio está en el centro y no conoce a nadie.
    [Fact]
    public void El_dominio_no_depende_de_ningun_otro_proyecto_ni_framework()
    {
        var referencias = typeof(Reto).Assembly.GetReferencedAssemblies().Select(a => a.Name!);

        Assert.DoesNotContain(referencias, r =>
            r.StartsWith("CommunityHub", StringComparison.Ordinal) ||
            r.StartsWith("Microsoft.EntityFrameworkCore", StringComparison.Ordinal) ||
            r.StartsWith("Microsoft.AspNetCore", StringComparison.Ordinal));
    }
}
