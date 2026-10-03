namespace CommunityHub.Api.Tests;

public class ArquitecturaTests
{
    // La API habla con el dominio a través de interfaces. EF Core es cosa de la infraestructura.
    [Fact]
    public void La_api_no_usa_entity_framework()
    {
        var referencias = typeof(Program).Assembly.GetReferencedAssemblies().Select(a => a.Name!);

        Assert.DoesNotContain(referencias, r => r.StartsWith("Microsoft.EntityFrameworkCore", StringComparison.Ordinal));
    }
}
