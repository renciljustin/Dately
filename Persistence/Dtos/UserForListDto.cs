using System;
using Dately.Shared.Enums;

namespace Dately.Persistence.Dtos
{
    public class UserForListDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}