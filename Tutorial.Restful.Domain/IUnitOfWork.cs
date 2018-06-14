using System.Threading.Tasks;

namespace Tutorial.Restful.Domain
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}