using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial.Restful.Controllers.Dto;
using Tutorial.Restful.Data;
using Tutorial.Restful.Domain.Models;
using Tutorial.Restful.Domain.Repositories;

namespace Tutorial.Restful.Host.Controllers
{
    [Route("api/countries/{countryId}/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityRepository cityRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCitiesAsync([FromRoute]int countryId)
        {
            if (!await _countryRepository.GetAll().AnyAsync(c => c.Id == countryId))
                return NotFound();
            var cities = await _cityRepository.GetAll().Where(c => c.CountryId == countryId).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<CityDto>>(cities));
        }

        // GET: api/Cities/5
        [HttpGet("{cityId}")]
        public async Task<ActionResult<CityDto>> GetCity([FromRoute]int countryId, [FromRoute] int cityId)
        {
            if (!await _countryRepository.GetAll().AnyAsync(c => c.Id == countryId))
                return NotFound();

            var city = await _cityRepository.GetAll().FirstOrDefaultAsync(c => c.CountryId == countryId && c.Id == cityId);

            if (city == null)
                return NotFound();

            return Ok(_mapper.Map<CityDto>(city));
        }

        //// PUT: api/Cities/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCity([FromRoute] int id, [FromBody] City city)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != city.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _cityRepository.Entry(city).State = EntityState.Modified;

        //    try
        //    {
        //        await _cityRepository.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CityExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Cities
        //[HttpPost]
        //public async Task<IActionResult> PostCity([FromBody] City city)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _cityRepository.Cities.Add(city);
        //    await _cityRepository.SaveChangesAsync();

        //    return CreatedAtAction("GetCity", new { id = city.Id }, city);
        //}

        //// DELETE: api/Cities/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCity([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var city = await _cityRepository.Cities.FindAsync(id);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    _cityRepository.Cities.Remove(city);
        //    await _cityRepository.SaveChangesAsync();

        //    return Ok(city);
        //}

        //private bool CityExists(int id)
        //{
        //    return _cityRepository.Cities.Any(e => e.Id == id);
        //}
    }
}