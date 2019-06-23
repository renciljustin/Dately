using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dately.Shared.Enums;
using Microsoft.AspNetCore.Identity;

namespace Dately.Core.Models
{
    public class User : IdentityUser, IModelBase
    {
        [Required]
        [StringLength(maximumLength: 255, MinimumLength=2, ErrorMessage="Must be at least 2 to 255 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 255, MinimumLength=2, ErrorMessage="Must be at least 2 to 255 characters.")]
        public string LastName { get; set; }

        public Gender? Gender { get; set; }

        [Column(TypeName = "DATE")]
        public DateTime? BirthDate { get; set; }

        public Gender? Interest { get; set; }

        public DateTime? CreationTime { get; set; }

        public DateTime? LastModified { get; set; }
        
        public bool? Flag { get; set; }

        public ICollection<UserRole> Roles { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}