using TestTask.Api.СachedСountries;

namespace TestTask.Api.Contracts
{
    public class CountryResponse
    {
        public CountryResponse(string code, string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(code);
            Code = code;
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            Name = name;
        }

        public string Code { get; }
        public string Name { get; }

        public static IEnumerable<CountryResponse> MapFrom(IEnumerable<FileCountry> countries)
        {
            return countries.Select(x => new CountryResponse(x.Code2, x.Name));
        }
    }
}
