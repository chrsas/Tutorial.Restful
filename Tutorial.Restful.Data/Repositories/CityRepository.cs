using System.Collections.Generic;
using System.Linq;
using Tutorial.Restful.Domain.Models;
using Tutorial.Restful.Domain.Repositories;

namespace Tutorial.Restful.Data.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly RestfulContext _restfulContext;

        public CityRepository(RestfulContext restfulContext)
        {
            _restfulContext = restfulContext;
        }


        public IQueryable<City> GetAll()
        {
            return _restfulContext.Cities;
        }

        public City Insert(City city)
        {
            return _restfulContext.Cities.Add(city).Entity;
        }
    }
}