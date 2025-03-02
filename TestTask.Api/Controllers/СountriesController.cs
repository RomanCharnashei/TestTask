using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Api.Contracts;
using TestTask.Api.СachedСountries;

namespace TestTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "MediumAccessLevel")]
        public IEnumerable<CountryResponse> GetAll([FromServices] ICountryFactory countryService)
        {
            var countries = countryService.GetCountries();

            return CountryResponse.MapFrom(countries);
        }
    }
}
