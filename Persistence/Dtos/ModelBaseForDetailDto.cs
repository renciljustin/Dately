using System;

namespace Dately.Persistence.Dtos
{
    public class ModelBaseForDetailDto
    {
        public DateTime? CreationTime { get; set; }
        public DateTime? LastModified { get; set; }
        public bool? Flag { get; set; }
    }
}