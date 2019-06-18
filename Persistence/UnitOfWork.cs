using System.Threading.Tasks;
using Dately.Core;

namespace Dately.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatelyDbContext _context;

        public UnitOfWork(DatelyDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}