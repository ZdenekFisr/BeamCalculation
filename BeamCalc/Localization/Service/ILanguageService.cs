namespace BeamCalc.Localization.Service
{
    public interface ILanguageService
    {
        Dictionary<string, string> GetLocalizedStrings(string resourceName, string languageCode);
    }
}