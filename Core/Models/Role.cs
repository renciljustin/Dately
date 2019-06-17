using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Dately.Core.Models
{
    public class Role : IdentityRole
    {
        public ICollection<UserRole> Users { get; set; }
    }
}