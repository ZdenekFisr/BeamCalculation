using BeamCalculation.Client.Services.EmbeddedCsv;

namespace BeamCalculation.Client.Localization.Service
{
    public class LanguageService(IEmbeddedCsvService embeddedCsvService) : ILanguageService
    {
        private readonly IEmbeddedCsvService _embeddedCsvService = embeddedCsvService;

        private readonly Dictionary<string, byte> languageColumns = new()
        {
            { "en", 1 },
            { "cs", 2 }
        };

        public Dictionary<string, string> GetLocalizedStrings(string resourceName, string languageCode)
        {
            bool isLanguageSupported = languageColumns.TryGetValue(languageCode, out byte languageColumn);
            if (!isLanguageSupported)
                languageColumn = 1;

            Dictionary<string, string> localizedStrings = _embeddedCsvService.ReadEmbeddedCsv(resourceName, 0, languageColumn);
            return localizedStrings;
        }
    }
}
