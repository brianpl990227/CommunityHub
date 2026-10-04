namespace CommunityHub.Api.Tests;

public class ArquitecturaTests
{
    private static readonly IEnumerable<string> ReferenciasDeLaApi =
        typeof(Program).Assembly.GetReferencedAssemblies().Select(a => a.Name!);

    // EF Core es cosa de la infraestructura.
    [Fact]
    public void La_api_no_usa_entity_framework() =>
        Assert.DoesNotContain(ReferenciasDeLaApi, r => r.StartsWith("Microsoft.EntityFrameworkCore", StringComparison.Ordinal));

    // Los endpoints pasan por los casos de uso: ni entidades ni repositorios.
    [Fact]
    public void La_api_no_usa_el_dominio() =>
        Assert.DoesNotContain("CommunityHub.Domain", ReferenciasDeLaApi);
}
