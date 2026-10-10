using System.Globalization;

namespace CommunityHub.UI.Formato;

public static class FechaExtensions
{
    extension(DateTimeOffset fecha)
    {
        // Fecha y hora con el formato del idioma del usuario.
        public string ParaMostrar => fecha.ToString("G", CultureInfo.CurrentCulture);
    }
}
