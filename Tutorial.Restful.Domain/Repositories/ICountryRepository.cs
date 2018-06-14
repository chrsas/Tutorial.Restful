using System.Linq;
using Tutorial.Restful.Domain.Models;

namespace Tutorial.Restful.Domain.Repositories
{
    public interface ICountryRepository
    {
        IQueryable<Country> GetAll();
    }
}