using System.Collections.Generic;
using System.Linq;
using Tutorial.Restful.Domain.Models;
using Tutorial.Restful.Domain.Repositories;

namespace Tutorial.Restful.Data.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly RestfulContext _restfulContext;

        public CountryRepository(RestfulContext restfulContext)
        {
            _restfulContext = restfulContext;
        }


        public IQueryable<Country> GetAll()
        {
            return _restfulContext.Countries;
        }

        public Country Insert(Country country)
        {
            return _restfulContext.Countries.Add(country).Entity;
        }
    }
}