using Bunit;
using CommunityHub.UI.Pages;

namespace CommunityHub.UI.Tests.Pages;

public class NotFoundTests : BunitContext
{
    [Fact]
    public void Muestra_el_encabezado_y_un_enlace_a_los_retos()
    {
        var cut = Render<NotFound>();

        var encabezado = Assert.Single(cut.FindAll("h1"));
        Assert.Equal("No encontrado", encabezado.TextContent);

        var enlace = cut.Find("a.volver-retos");

        Assert.Equal("/retos", enlace.GetAttribute("href"));
        Assert.Equal("Volver a los retos", enlace.TextContent.Trim());
    }
}
