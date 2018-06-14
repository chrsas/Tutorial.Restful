using System.Threading.Tasks;
using Tutorial.Restful.Domain;

namespace Tutorial.Restful.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestfulContext _restfulContext;

        public UnitOfWork(RestfulContext restfulContext)
        {
            _restfulContext = restfulContext;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _restfulContext.SaveChangesAsync();
        }
    }
}