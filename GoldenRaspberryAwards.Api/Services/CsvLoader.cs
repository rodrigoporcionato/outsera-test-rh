using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using GoldenRaspberryAwards.Api.Data;
using GoldenRaspberryAwards.Api.Entities;

namespace GoldenRaspberryAwards.Api.Services
{
    public class CsvLoader(AppDbContext context, string csvFilePath)
    {
        private readonly AppDbContext _context = context;
        private readonly string _csvFilePath = csvFilePath;

        public void LoadCsvToDatabase()
        {
            if (!File.Exists(_csvFilePath))
            {
                throw new FileNotFoundException($"O arquivo CSV '{_csvFilePath}' não foi encontrado.");
            }

             var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",  // Usando ponto e vírgula como delimitador
                HeaderValidated = null,  // Ignorando validação de cabeçalho
                MissingFieldFound = null,  // Ignorando campos faltantes
            };

            using var reader = new StreamReader(_csvFilePath);
            using var csv = new CsvReader(reader,config);

            csv.Context.RegisterClassMap<MovieMap>();
            
            var movies = csv.GetRecords<Movie>().ToList();

            _context.Movies.AddRange(movies);
            _context.SaveChanges();
        }
    }
}
