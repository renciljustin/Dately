using System;
using System.ComponentModel.DataAnnotations;
using Dately.Shared.Enums;

namespace Dately.Persistence.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength=3, ErrorMessage="Must be at least 2 to 255 characters.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength=8, ErrorMessage="Must be at least 2 to 255 characters.")]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 255, MinimumLength=2, ErrorMessage="Must be at least 2 to 255 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 255, MinimumLength=2, ErrorMessage="Must be at least 2 to 255 characters.")]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public Gender Interest { get; set; }
    }
}