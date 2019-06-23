using System;

namespace Dately.Core
{
    public interface IModelBase
    {
        DateTime? CreationTime { get; set; }
        DateTime? LastModified { get; set; }
        bool? Flag { get; set; }
    }
}