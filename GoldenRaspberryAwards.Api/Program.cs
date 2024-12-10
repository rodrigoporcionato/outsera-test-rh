using Microsoft.EntityFrameworkCore;
using GoldenRaspberryAwards.Api.Data;
using GoldenRaspberryAwards.Api.Services;

// var builder = WebApplication.CreateBuilder(args);

// // Configurar banco de dados em memória (H2 usando EF Core InMemory)
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseInMemoryDatabase("GoldenRaspberryAwards"));

// // Configuração do AutoMapper
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// // Caminho para o arquivo CSV
// string csvFilePath = Path.Combine(AppContext.BaseDirectory, "../Artefatos/movielist.csv");

// // Registrar o CsvLoader no contêiner de injeção
// builder.Services.AddScoped<CsvLoader>(provider =>
// {
//     var context = provider.GetRequiredService<AppDbContext>();
//     return new CsvLoader(context, csvFilePath);
// });

// // Configuração de controllers e validações
// builder.Services.AddControllers();

// // Configuração do Swagger para documentação
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configuração do pipeline HTTP
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// // Carregar os dados do CSV ao iniciar
// using (var scope = app.Services.CreateScope())
// {
//     var loader = scope.ServiceProvider.GetRequiredService<CsvLoader>();
//     loader.LoadCsvToDatabase();
// }

// app.UseHttpsRedirection();
// app.UseAuthorization();
// app.MapControllers();

// app.Run();


// public class Program
// {
//     public static void Main(string[] args)
//     {
//         var builder = WebApplication.CreateBuilder(args);

//         // Configurar banco de dados em memória (H2 usando EF Core InMemory)
//         builder.Services.AddDbContext<AppDbContext>(options =>
//             options.UseInMemoryDatabase("GoldenRaspberryAwards"));

//         // Configuração do AutoMapper
//         builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//         // Caminho para o arquivo CSV
//         string csvFilePath = Path.Combine(AppContext.BaseDirectory, "../Artefatos/movielist.csv");

//         // Registrar o CsvLoader no contêiner de injeção
//         builder.Services.AddScoped<CsvLoader>(provider =>
//         {
//             var context = provider.GetRequiredService<AppDbContext>();
//             return new CsvLoader(context, csvFilePath);
//         });

//         // Configuração de controllers e validações
//         builder.Services.AddControllers();

//         // Configuração do Swagger para documentação
//         builder.Services.AddEndpointsApiExplorer();
//         builder.Services.AddSwaggerGen();

//         var app = builder.Build();

//         // Configuração do pipeline HTTP
//         if (app.Environment.IsDevelopment())
//         {
//             app.UseSwagger();
//             app.UseSwaggerUI();
//         }

//         // Carregar os dados do CSV ao iniciar
//         using (var scope = app.Services.CreateScope())
//         {
//             var loader = scope.ServiceProvider.GetRequiredService<CsvLoader>();
//             loader.LoadCsvToDatabase();
//         }

//         app.UseHttpsRedirection();
//         app.UseAuthorization();
//         app.MapControllers();

//         app.Run();
//     }
// }


var builder = WebApplication.CreateBuilder(args);

// Configurar banco de dados em memória (H2 usando EF Core InMemory)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("GoldenRaspberryAwards"));

// Configuração do AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Caminho para o arquivo CSV
string csvFilePath = Path.Combine(AppContext.BaseDirectory, "movielist.csv");

// Registrar o CsvLoader no contêiner de injeção
builder.Services.AddScoped<CsvLoader>(provider =>
{
    var context = provider.GetRequiredService<AppDbContext>();
    return new CsvLoader(context, csvFilePath);
});

// Configuração de controllers e validações
builder.Services.AddControllers();

// Configuração do Swagger para documentação
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Carregar os dados do CSV ao iniciar
using (var scope = app.Services.CreateScope())
{
    var loader = scope.ServiceProvider.GetRequiredService<CsvLoader>();
    loader.LoadCsvToDatabase();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


public partial class Program { }
