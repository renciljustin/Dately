using System;

namespace Dately.Persistence.Dtos
{
    public class RefreshTokenForDisplayDto
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}