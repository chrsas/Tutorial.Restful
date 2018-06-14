using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial.Restful.Controllers.Dto;
using Tutorial.Restful.Data;
using Tutorial.Restful.Domain.Models;
using System.Linq;
using AutoMapper;

namespace Tutorial.Restful.Host.Controllers
{
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {
        private readonly RestfulContext _restfulContext;

        private readonly IMapper _mapper;

        public CountriesController(RestfulContext restfulContext, IMapper mapper)
        {
            _restfulContext = restfulContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryDto>> Get()
        {
            return _mapper.Map<IEnumerable<CountryDto>>(await _restfulContext.Countries.ToListAsync());
        }
    }
}