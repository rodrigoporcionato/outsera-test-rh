using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using GoldenRaspberryAwards.Api.Data;
using GoldenRaspberryAwards.Api.Entities;
using GoldenRaspberryAwards.Api.Controllers;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberryAwards.Api.Tests
{


    public class ProducerInterval
    {
        public string Producer { get; set; }
        public int Interval { get; set; }
        public int PreviousWin { get; set; }
        public int FollowingWin { get; set; }
    }

    public class ProducersWithIntervalsResult
    {
        public List<ProducerInterval> Min { get; set; }
        public List<ProducerInterval> Max { get; set; }
    }




    public class MoviesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        // Construtor público
       
        public MoviesControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
            public async Task GetProducersWithIntervals_ShouldReturnCorrectData()
    {
        // Arrange: Criar uma instância do cliente HTTP
        var client = _factory.CreateClient();

        // Adicionar filmes no banco em memória
        var context = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        context.Movies.AddRange(new Movie
        {
            Year = 2000,
            Title = "Movie 1",
            Studios = "Studio A",
            Producers = "Producer 1",
            Winner = true
        }, new Movie
        {
            Year = 2005,
            Title = "Movie 2",
            Studios = "Studio B",
            Producers = "Producer 1",
            Winner = true
        }, new Movie
        {
            Year = 2010,
            Title = "Movie 3",
            Studios = "Studio C",
            Producers = "Producer 2",
            Winner = true
        }, new Movie
        {
            Year = 2020,
            Title = "Movie 4",
            Studios = "Studio D",
            Producers = "Producer 2",
            Winner = true
        });
        await context.SaveChangesAsync();

        // Act: Chamar o endpoint de intervalo dos produtores
        var response = await client.GetAsync("/api/movies/producers/intervals");

        // Assert: Verificar se o status code é 200 OK
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // Usar ReadFromJsonAsync para desserializar o JSON para um tipo fortemente tipado
        var result = await response.Content.ReadFromJsonAsync<ProducersWithIntervalsResult>();

        // Verificar se o produtor 1 tem intervalo entre os prêmios de 5 anos
        var producer1 = result.Min.FirstOrDefault(p => p.Producer == "Producer 1");
        producer1.Should().NotBeNull();
        producer1.Interval.Should().Be(5);
        producer1.PreviousWin.Should().Be(2000); // Verificando o ano do prêmio anterior
        producer1.FollowingWin.Should().Be(2005); // Verificando o ano do prêmio seguinte

        // Verificar se o produtor 2 tem intervalo entre os prêmios de 10 anos
        var producer2 = result.Max.FirstOrDefault(p => p.Producer == "Producer 2");
        producer2.Should().NotBeNull();
        producer2.Interval.Should().Be(10);
        producer2.PreviousWin.Should().Be(2010); // Verificando o ano do prêmio anterior
        producer2.FollowingWin.Should().Be(2020); // Verificando o ano do prêmio seguinte
    }


        // Teste para garantir que o CSV está sendo carregado corretamente e os dados são salvos
        [Fact]
        public async Task CsvLoader_ShouldLoadDataToDatabase()
        {
            // Arrange: Criar uma instância do cliente HTTP
            var client = _factory.CreateClient();

            // Verificar se o banco de dados contém filmes após a carga
            var context = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
            var movieCount = await context.Movies.CountAsync();

            // Assert: Verificar se pelo menos um filme foi carregado
            movieCount.Should().BeGreaterThan(0);
        }
    }
}
