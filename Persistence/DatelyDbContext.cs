using Dately.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dately.Persistence
{
    public class DatelyDbContext : IdentityDbContext<User>
    {
        public DatelyDbContext(DbContextOptions<DatelyDbContext> options): base(options)
        {
            
        }
    }
}