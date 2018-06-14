using System.Linq;
using Tutorial.Restful.Domain.Models;

namespace Tutorial.Restful.Domain.Repositories
{
    public interface ICityRepository
    {
        IQueryable<City> GetAll();

        City Insert(City country);
    }
}