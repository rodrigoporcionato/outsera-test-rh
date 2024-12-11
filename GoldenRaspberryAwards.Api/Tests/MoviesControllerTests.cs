using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using GoldenRaspberryAwards.Api.Data;
using GoldenRaspberryAwards.Api.Entities;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberryAwards.Api.Tests
{
    public class MoviesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public MoviesControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddDbContext<AppDbContext>(options =>
                        options.UseInMemoryDatabase("InMemoryDbForTesting"));

                    // Garantir que o banco de dados seja inicializado vazio
                    using var scope = services.BuildServiceProvider().CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    context.Database.EnsureCreated();
                });
            });
        }

        [Fact]
        public async Task GetProducersWithIntervals_ShouldReturnCorrectData()
        {
            var client = _factory.CreateClient();

            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
               
                await context.SaveChangesAsync();
            }

            var response = await client.GetAsync("/api/movies/producers/intervals");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<ProducersWithIntervalsResult>();

            var producer1 = result.Min.FirstOrDefault(p => p.Producer == "Lorenzo di Bonaventura");
            producer1.Should().NotBeNull();
            producer1?.Interval.Should().Be(0);
            producer1?.PreviousWin.Should().Be(2009);
            producer1?.FollowingWin.Should().Be(2011);

            var producer2 = result.Max.FirstOrDefault(p => p.Producer == "Renny Harlin");
            producer2.Should().NotBeNull();
            producer2?.Interval.Should().Be(13);
            producer2?.PreviousWin.Should().Be(2001);
            producer2?.FollowingWin.Should().Be(2014);
        }
    }
}
