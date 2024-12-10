using CsvHelper.Configuration;
using GoldenRaspberryAwards.Api.Entities;
using CsvHelper.TypeConversion;

public sealed class MovieMap : ClassMap<Movie>
{
    public MovieMap()
    {
        Map(m => m.Year).Name("year");
        Map(m => m.Title).Name("title");
        Map(m => m.Studios).Name("studios");
        Map(m => m.Producers).Name("producers");

       
    }
}
