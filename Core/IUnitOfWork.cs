using System.Threading.Tasks;

namespace Dately.Core
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}