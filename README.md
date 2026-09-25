# CommunityHub

La app de la comunidad [DotNet Desarrolladores](https://t.me/DotNetDesarrolladoresGrupo), construida en público.

De octubre a diciembre de 2026 voy a construir aquí una app completa con Blazor, ASP.NET Core y .NET MAUI, con sus errores incluidos. Cada viernes cuento en [LinkedIn](https://www.linkedin.com/in/brian-perez-lopez/) qué avancé, qué se rompió y qué aprendí.

Como hicimos con [PetLand](https://github.com/Desarrolladores-Net/PetLand), pero esta vez en público y con el stack completo de .NET.

## ¿Qué va a hacer?

Retos de código cada semana y un ranking para ver quién va primero. Lo mismo que hacemos hoy a mano en el grupo, pero con su propia app:

- Listado y detalle de los retos
- Envío de soluciones
- Ranking en tiempo real
- Login
- App móvil con la misma interfaz que la web (Android y Windows)

## Con qué

| Proyecto | Tecnología |
|---|---|
| `CommunityHub.Web` | Blazor Web App |
| `CommunityHub.Shared.UI` | Razor Class Library, compartida entre la web y el móvil |
| `CommunityHub.Api` | ASP.NET Core con Minimal APIs, Identity y SignalR |
| `CommunityHub.Maui` | .NET MAUI Blazor Hybrid |
| Datos | EF Core + SQLite |
| `CommunityHub.AppHost` | Aspire (opcional, llega al final) |

Arranca en .NET 10 y en noviembre la migramos a .NET 11.

## Estado

🚧 **Semana 0.** Todavía no hay código: la solución empieza a mediados de octubre.

| Semana | Qué se construye |
|---|---|
| 12 – 16 oct | Estructura de la solución y la librería de componentes compartida |
| Octubre | Retos, envío de soluciones y validación |
| 2 – 6 nov | Login con Identity y passkeys |
| 16 – 20 nov | Migración a .NET 11 |
| 23 – 27 nov | Ranking en tiempo real con SignalR |
| 7 – 11 dic | La app en el móvil con MAUI Blazor Hybrid |
| 14 – 18 dic | Aspire |

## Cómo participar

- **Entra al [grupo de Telegram](https://t.me/DotNetDesarrolladoresGrupo)** y cuéntanos qué le pondrías a la app. Las funcionalidades se votan ahí.
- **En octubre hay Hacktoberfest:** voy a abrir issues con la etiqueta `good first issue` para que puedas hacer tu primer PR aquí. Los PRs van a [este repo](https://github.com/brianpl990227/CommunityHub); el de la organización es un fork.
- **¿Ves algo raro en el código?** Abre un issue. Si te equivocas tú, no pasa nada; si me equivoco yo, mejor enterarme pronto 😅

## Para ejecutarlo

Cuando haya código, solo vas a necesitar el [SDK de .NET 10](https://dotnet.microsoft.com/download):

```bash
git clone https://github.com/brianpl990227/CommunityHub.git
cd CommunityHub
dotnet run --project src/CommunityHub.Web
```

La base de datos es SQLite, así que no hace falta Docker. Es a propósito: mucha gente de la comunidad no puede usarlo, y la app tiene que arrancar en cualquier máquina.

## Licencia

[MIT](LICENSE). Úsalo, cópialo y aprende con él.
