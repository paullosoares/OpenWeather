using System.Threading.Tasks;

namespace Weather.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
