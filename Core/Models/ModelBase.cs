using System;

namespace Dately.Core.Models
{
    public class ModelBase
    {
        public DateTime? CreationTime { get; set; }
        public DateTime? LastModified { get; set; }
        public bool? Flag { get; set; }
    }
}