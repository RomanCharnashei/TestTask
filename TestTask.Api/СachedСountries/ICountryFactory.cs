
namespace TestTask.Api.СachedСountries
{
    public interface ICountryFactory
    {
        IEnumerable<FileCountry> GetCountries();
        FileCountry GetCountryByCode(string code);
    }
}