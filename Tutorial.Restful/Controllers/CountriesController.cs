using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial.Restful.Controllers.Dto;
using Tutorial.Restful.Data;
using Tutorial.Restful.Domain.Models;
using System.Linq;
using AutoMapper;
using Tutorial.Restful.Domain.Repositories;

namespace Tutorial.Restful.Host.Controllers
{
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {
        private readonly IMapper _mapper;

        private readonly ICountryRepository _countryRepository;

        public CountriesController(IMapper mapper, ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<CountryDto>> Get()
        {
            return _mapper.Map<IEnumerable<CountryDto>>(await _countryRepository.GetAll().ToListAsync());
        }
    }
}