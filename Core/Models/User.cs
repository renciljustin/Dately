using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Dately.Core.Models
{
    public enum Gender {
        Male=0, Female=1
    }

    public class User : IdentityUser
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
    }
}