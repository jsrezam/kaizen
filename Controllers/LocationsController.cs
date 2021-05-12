using System.Threading.Tasks;
using AutoMapper;
using Kaizen.Core.DTOs;
using Kaizen.Core.Interfaces;
using Kaizen.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Controllers
{
    [Route("/api/locations")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LocationsController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICountryService countryService;
        private readonly IRegionService regionService;
        private readonly ICityService cityService;

        public LocationsController(IMapper mapper,
        ICountryService countryService,
        IRegionService regionService,
        ICityService cityService)
        {
            this.cityService = cityService;
            this.regionService = regionService;
            this.countryService = countryService;
            this.mapper = mapper;
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetCountriesAsync()
        {
            var queryResult = await countryService.GetCountriesAsync();
            var resultQuery = mapper.Map<QueryResult<Country>, QueryResultDto<CountryDto>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("regions/{countryId}")]
        public async Task<IActionResult> GetRegionsByCountryAsync(int countryId)
        {
            var queryResult = await regionService.GetRegionsByCountryAsync(countryId);
            var resultQuery = mapper.Map<QueryResult<Region>, QueryResultDto<RegionDto>>(queryResult);
            return Ok(resultQuery);
        }

        [HttpGet("cities/{regionId}")]
        public async Task<IActionResult> GetCitiesByRegionAsync(int regionId)
        {
            var queryResult = await cityService.GetCitiesByRegionAsync(regionId);
            var resultQuery = mapper.Map<QueryResult<City>, QueryResultDto<CityDto>>(queryResult);
            return Ok(resultQuery);
        }

    }
}