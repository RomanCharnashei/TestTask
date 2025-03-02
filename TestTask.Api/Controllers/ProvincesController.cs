using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Api.Contracts;
using TestTask.Api.СachedСountries;

namespace TestTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvincesController : ControllerBase
    {
        [HttpGet("{countryСode}")]
        [Authorize(Policy = "MediumAccessLevel")]
        public IEnumerable<ProvinceResponse> GetCountryProvinces(
            [FromRoute] string countryСode,
            [FromServices] ICountryFactory countryService)
        {
            var country = countryService.GetCountryByCode(countryСode);

            return ProvinceResponse.MapFrom(country.States);
        }
    }
}
