using Microsoft.Extensions.Caching.Memory;
using System.Collections.Immutable;
using System.Text.Json;

namespace TestTask.Api.СachedСountries
{
    public class СountryCacheInitializer : IHostedService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<СountryCacheInitializer> _logger;

        public СountryCacheInitializer(
            IMemoryCache cache,
            ILogger<СountryCacheInitializer> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var filePath = "countries.json";

            if (!File.Exists(filePath))
            {
                _logger.LogError("The countries.json file was not found");
                return;
            }

            try
            {
                var json = await File.ReadAllTextAsync(filePath, cancellationToken);
                var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var fileCountries = JsonSerializer.Deserialize<ImmutableArray<FileCountry>>(json, jsonOptions);
                //var countries = CountryResponse.MapFrom(fileCountries);

                _cache.Set("CountriesData", fileCountries, new MemoryCacheEntryOptions
                {
                    Priority = CacheItemPriority.NeverRemove
                });

                _logger.LogInformation("Country data loaded into cache");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading data into cache");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) 
            => Task.CompletedTask;
    }
}
