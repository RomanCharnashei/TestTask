using Microsoft.Extensions.Caching.Memory;
using System.Collections.Immutable;
using TestTask.Api.Infrastructure;

namespace TestTask.Api.СachedСountries
{
    public class CountryFactory : ICountryFactory
    {
        private readonly IMemoryCache _cache;

        public CountryFactory(IMemoryCache cache)
        {
            _cache = cache;
        }

        public IEnumerable<FileCountry> GetCountries()
        {
            if (_cache.TryGetValue("CountriesData", out ImmutableArray<FileCountry> countries))
            {
                return countries;
            }

            throw new InvalidOperationException("Country data is not uploaded");
        }

        public FileCountry GetCountryByCode(string code)
        {
            if (_cache.TryGetValue("CountriesData", out ImmutableArray<FileCountry> countries))
            {
                return countries.FirstOrDefault(x => x.Code2 == code)
                    ?? throw new NotFoundException("Country", code);
            }

            throw new InvalidOperationException("Country data is not uploaded");
        }
    }
}
