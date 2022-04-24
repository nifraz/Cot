using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cot.Web.Core.Domain
{
    public class Course : IEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public CourseType Type { get; set; }
        public DateTime? AddedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }

    public enum CourseType : byte
    {
        None = 0,
        NVQ1 = 1,
        NVQ2 = 2,
        NVQ3 = 3,
        NVQ4 = 4,
        NVQ5 = 5,
        NVQ6 = 6,
        NVQ7 = 7
    }
}
