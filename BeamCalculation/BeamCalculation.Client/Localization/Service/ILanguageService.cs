namespace BeamCalculation.Client.Localization.Service
{
    public interface ILanguageService
    {
        Dictionary<string, string> GetLocalizedStrings(string resourceName, string languageCode);
    }
}