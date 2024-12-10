using Microsoft.EntityFrameworkCore;
using GoldenRaspberryAwards.Api;

namespace GoldenRaspberryAwards.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<GoldenRaspberryAwards.Api.Entities.Movie> Movies { get; set; } // Tabela para os filmes
    }
}
