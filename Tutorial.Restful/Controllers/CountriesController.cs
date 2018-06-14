using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial.Restful.Controllers.Dto;
using Tutorial.Restful.Data;
using Tutorial.Restful.Domain.Models;
using System.Linq;
using AutoMapper;
using Tutorial.Restful.Domain;
using Tutorial.Restful.Domain.Repositories;

namespace Tutorial.Restful.Host.Controllers
{
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {
        private readonly IMapper _mapper;

        private readonly ICountryRepository _countryRepository;

        private readonly IUnitOfWork _unitOfWork;

        public CountriesController(IMapper mapper, ICountryRepository countryRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Post([FromBody]CountryDto countryDto)
        {
            if (_countryRepository.GetAll().Any(c => c.EnglishName == countryDto.EnglishName))
            {
                ModelState.AddModelError(nameof(CountryDto.EnglishName), $"已经存在名为 {countryDto.EnglishName} 的国家");
                return BadRequest(ModelState);
            }
            var country = _mapper.Map<Country>(countryDto);
            _countryRepository.Insert(country);
            _unitOfWork.SaveAsync().Wait();
            return Ok();
        }

        public async Task<IEnumerable<CountryDto>> Get()
        {
            return _mapper.Map<IEnumerable<CountryDto>>(await _countryRepository.GetAll().ToListAsync());
        }

        [HttpGet("{id}")]
        public CountryDto Get(int id)
        {
            var country = _countryRepository.GetAll().FirstOrDefault(c => c.Id == id);
            return _mapper.Map<CountryDto>(country);
        }
    }
}