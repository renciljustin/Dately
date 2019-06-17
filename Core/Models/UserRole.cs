using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Dately.Core.Models
{
    public class UserRole: IdentityUserRole<string>
    {
         public User User { get; set; }
         public Role Role { get; set; }
    }
}