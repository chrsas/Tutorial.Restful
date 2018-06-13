using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial.Restful.Data;
using Tutorial.Restful.Domain.Models;

namespace Tutorial.Restful.Host.Controllers
{
    [Route("api/[controller]")]
    public class CountiesController : Controller
    {
        private readonly RestfulContext _restfulContext;

        public CountiesController(RestfulContext restfulContext)
        {
            _restfulContext = restfulContext;
        }

        public async Task<IEnumerable<Country>> Get()
        {
            return await _restfulContext.Countries.ToListAsync();
        }
    }
}