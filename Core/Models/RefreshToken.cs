using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dately.Core.Models
{
    [Table("AspNetRefreshTokens")]
    public class RefreshToken
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public bool Revoked { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}