using Microsoft.AspNetCore.Mvc;
using GoldenRaspberryAwards.Api.Data;
using GoldenRaspberryAwards.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GoldenRaspberryAwards.Api.Controllers
{
 
 [ApiController]
[Route("api/[controller]")]
public class MoviesController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
    {
        var movies = await _context.Movies.ToListAsync();
        return Ok(movies);
    }



        [HttpGet("producers/intervals")]
        public async Task<IActionResult> GetProducersWithIntervals()
        {
            // Obter todos os filmes vencedores
            var winners = await _context.Movies.ToListAsync();

        
        // Agrupar filmes por produtor e ordenar por ano de premiação
    var producerIntervals = winners
        .SelectMany(m => m.Producers.Split(", "), (m, producer) => new { Producer = producer, Year = m.Year })
        .GroupBy(p => p.Producer)
        .Select(g => new
        {
            Producer = g.Key,
            Years = g.OrderBy(p => p.Year).Select(p => p.Year).ToList()
        })
        .Select(p => new
        {
            p.Producer,
            // Calcular os intervalos entre os anos de premiação
            Intervals = p.Years.Zip(p.Years.Skip(1), (prev, next) => next - prev).ToList(),
            Years = p.Years // Incluindo os anos de premiação
        })
        .Where(p => p.Intervals.Count > 0) // Filtrando produtores com intervalos válidos
        .ToList();

    // Se não houver produtores com intervalos válidos
    if (!producerIntervals.Any())
    {
        return NotFound("Nenhum produtor com intervalo encontrado.");
    }

    // Encontrar o menor e maior intervalo
    var minInterval = producerIntervals.Min(p => p.Intervals.Min());
    var maxInterval = producerIntervals.Max(p => p.Intervals.Max());

    // Obter os produtores com o menor intervalo (min) e o maior intervalo (max)
    var result = new
    {
        Min = producerIntervals
            .Where(p => p.Intervals.Min() == minInterval)
            .Select(p => new
            {
                p.Producer,
                Interval = p.Intervals.Min(),
                PreviousWin = p.Years.First(),
                FollowingWin = p.Years.Last(),
                p.Years // Incluindo os anos no resultado
            }),
        Max = producerIntervals
            .Where(p => p.Intervals.Max() == maxInterval)
            .Select(p => new
            {
                p.Producer,
                Interval = p.Intervals.Max(),
                PreviousWin = p.Years.First(),
                FollowingWin = p.Years.Last(),
                p.Years // Incluindo os anos no resultado
            })
    };

            return Ok(result);
        }


    



    
}
   
 
 
}
