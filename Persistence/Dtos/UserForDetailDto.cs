using System;
using Dately.Shared.Enums;

namespace Dately.Persistence.Dtos
{
    public class UserForDetailDto: ModelBaseForDetailDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public Gender? Interest { get; set; }
    }
}