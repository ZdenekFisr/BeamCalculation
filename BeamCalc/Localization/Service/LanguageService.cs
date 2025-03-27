using BeamCalc.Services.EmbeddedCsv;

namespace BeamCalc.Localization.Service
{
    public class LanguageService : ILanguageService
    {
        private readonly IEmbeddedCsvService _embeddedCsvService;

        private readonly Dictionary<string, byte> languageColumns;

        public LanguageService(IEmbeddedCsvService embeddedCsvService)
        {
            _embeddedCsvService = embeddedCsvService;

            languageColumns = [];
            foreach (string languageCode in Constants.SupportedLanguages)
            {
                if (!languageColumns.ContainsKey(languageCode))
                    languageColumns.Add(languageCode, (byte)(languageColumns.Count + 1));
            }
        }

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
