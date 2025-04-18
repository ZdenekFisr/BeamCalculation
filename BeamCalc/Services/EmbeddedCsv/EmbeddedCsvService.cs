﻿using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Reflection;

namespace BeamCalc.Services.EmbeddedCsv
{
    /// <summary>
    /// Service for reading embedded CSV files.
    /// </summary>
    public class EmbeddedCsvService : IEmbeddedCsvService
    {
        /// <inheritdoc />
        public Dictionary<string, string> ReadEmbeddedCsv(string resourceName, int keyColumn, int valueColumn)
        {
            var assembly = Assembly.GetCallingAssembly();
            var config = new CsvConfiguration(new CultureInfo("en-US"))
            {
                Delimiter = ";",
                HasHeaderRecord = false
            };
            using Stream? stream = assembly.GetManifestResourceStream(resourceName);
            if (stream is null)
                return [];
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, config);
            var records = new Dictionary<string, string>();
            while (csv.Read())
            {
                if (csv.TryGetField(keyColumn, out string? key)
                    && csv.TryGetField(valueColumn, out string? value)
                    && key is not null
                    && value is not null)
                {
                    records[key] = value;
                }
            }
            return records;
        }
    }
}
