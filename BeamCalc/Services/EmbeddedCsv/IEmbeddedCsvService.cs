namespace BeamCalc.Services.EmbeddedCsv
{
    /// <summary>
    /// Service for reading embedded CSV files.
    /// </summary>
    public interface IEmbeddedCsvService
    {
        /// <summary>
        /// Reads an embedded CSV file and returns a dictionary with specified key and value columns.
        /// </summary>
        /// <param name="resourceName">The name of the embedded CSV resource.</param>
        /// <param name="keyColumn">The index of the column to be used as keys in the dictionary.</param>
        /// <param name="valueColumn">The index of the column to be used as values in the dictionary.</param>
        /// <returns>A dictionary with keys and values from the specified columns of the CSV file.</returns>
        Dictionary<string, string> ReadEmbeddedCsv(string resourceName, int keyColumn, int valueColumn);
    }
}
