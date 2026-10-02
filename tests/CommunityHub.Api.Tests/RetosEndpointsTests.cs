using System.Net;
using System.Net.Http.Json;
using CommunityHub.Contracts.Retos;
using Microsoft.AspNetCore.Http;

namespace CommunityHub.Api.Tests;

public class RetosEndpointsTests(CommunityHubApiFactory factory) : IClassFixture<CommunityHubApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Crear_un_reto_valido_devuelve_201_y_se_puede_consultar()
    {
        var request = new CrearRetoRequest { Titulo = "FizzBuzz", Enunciado = "Del 1 al 100.", Puntos = 10 };

        var response = await _client.PostAsJsonAsync("/retos", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var creado = await response.Content.ReadFromJsonAsync<RetoResponse>();
        Assert.NotNull(creado);
        Assert.Equal($"/retos/{creado.Id}", response.Headers.Location?.AbsolutePath);

        var consultado = await _client.GetFromJsonAsync<RetoResponse>($"/retos/{creado.Id}");
        Assert.Equal("FizzBuzz", consultado?.Titulo);

        var todos = await _client.GetFromJsonAsync<RetoResponse[]>("/retos");
        Assert.Contains(todos!, r => r.Id == creado.Id);
    }

    [Fact]
    public async Task Crear_un_reto_invalido_devuelve_400_con_los_errores()
    {
        var request = new CrearRetoRequest { Titulo = "", Enunciado = "Algo", Puntos = 500 };

        var response = await _client.PostAsJsonAsync("/retos", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
        var problema = await response.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();
        Assert.Contains("Titulo", problema!.Errors.Keys);
        Assert.Contains("Puntos", problema.Errors.Keys);
    }

    [Fact]
    public async Task Pedir_un_reto_que_no_existe_devuelve_404()
    {
        var response = await _client.GetAsync("/retos/99999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Health_responde_Healthy()
    {
        var response = await _client.GetAsync("/health");

        response.EnsureSuccessStatusCode();
        Assert.Equal("Healthy", await response.Content.ReadAsStringAsync());
    }
}
