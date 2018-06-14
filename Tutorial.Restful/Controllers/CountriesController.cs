using System;
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
        public async Task<IActionResult> Post([FromBody]CountryAddDto countryAddDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _countryRepository.GetAll().AnyAsync(c => c.EnglishName == countryAddDto.EnglishName))
            {
                ModelState.AddModelError(nameof(CountryDto.EnglishName), $"已经存在名为 {countryAddDto.EnglishName} 的国家");
                return BadRequest(ModelState);
            }

            var country = _mapper.Map<Country>(countryAddDto);
            _countryRepository.Insert(country);
            await _unitOfWork.SaveChangesAsync();
            var countryDto = _mapper.Map<CountryDto>(country);
            // CreatedAtAction 会返回绝对路径，实际项目中绝对路径会被Api网关转换。
            //return CreatedAtAction("Get", new { id = country.Id }, countryDto);
            return Created(new Uri($"{Request.Path}/{countryDto.Id}", UriKind.Relative), countryDto);
        }

        public async Task<IEnumerable<CountryDto>> Get()
        {
            return _mapper.Map<IEnumerable<CountryDto>>(await _countryRepository.GetAll().ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> Get(int id)
        {
            var country = await _countryRepository.GetAll().FirstOrDefaultAsync(c => c.Id == id);
            if (country == null)
                return NotFound();
            return Ok(_mapper.Map<CountryDto>(country));
        }

        [HttpGet("exception")]
        public IActionResult GetException()
        {
            throw new Exception("");
        }
    }
}