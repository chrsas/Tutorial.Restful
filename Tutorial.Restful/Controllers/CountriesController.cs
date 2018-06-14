using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial.Restful.Controllers.Dto;
using Tutorial.Restful.Data;
using Tutorial.Restful.Domain.Models;
using System.Linq;

namespace Tutorial.Restful.Host.Controllers
{
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {
        private readonly RestfulContext _restfulContext;

        public CountriesController(RestfulContext restfulContext)
        {
            _restfulContext = restfulContext;
        }

        public async Task<IEnumerable<CountryDto>> Get()
        {
            return await _restfulContext.Countries.Select(c => new CountryDto
            {
                Id = c.Id,
                Abbreviation = c.Abbreviation,
                ChineseName = c.ChineseName,
                Continent = c.Continent,
                EnglishName = c.EnglishName
            }).ToListAsync();
        }
    }
}