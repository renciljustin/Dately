using System.ComponentModel.DataAnnotations;

namespace Dately.Persistence.Dtos
{
    public class UserForLoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}