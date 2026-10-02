using System.Data.Common;
using CommunityHub.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CommunityHub.Api.Tests;

/// <summary>
/// Levanta la API con una base de datos SQLite en memoria para cada ejecución de tests
/// (https://learn.microsoft.com/aspnet/core/test/integration-tests#customize-webapplicationfactory).
/// </summary>
public sealed class CommunityHubApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        builder.ConfigureTestServices(services =>
        {
            services.Remove(services.Single(d =>
                d.ServiceType == typeof(IDbContextOptionsConfiguration<CommunityHubDbContext>)));

            services.AddSingleton<DbConnection>(_ =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();
                return connection;
            });

            services.AddDbContext<CommunityHubDbContext>((container, options) =>
                options.UseSqlite(container.GetRequiredService<DbConnection>()));
        });
    }
}
