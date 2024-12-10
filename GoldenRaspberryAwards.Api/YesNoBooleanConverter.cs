using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

public class YesNoBooleanConverter : ITypeConverter
{
    public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
         if (string.Equals(text, "yes", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        else if (string.Equals(text, "no", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        // Caso o valor não seja "yes" ou "no", lança uma exceção
        throw new InvalidOperationException($"Cannot convert '{text}' to boolean.");
    }

    public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is bool boolValue)
        {
            return boolValue ? "yes" : "no";
        }
            throw new InvalidOperationException("Cannot convert value to string.");    
    }
}
