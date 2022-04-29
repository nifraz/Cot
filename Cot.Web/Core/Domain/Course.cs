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
        public CourseLevel Level { get; set; }
        public CourseType Type { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Notes { get; set; }
    }

    public enum CourseLevel : byte
    {
        None = 0,
        NVQ4 = 4,
        NVQ5 = 5,
        NVQ6 = 6
    }

    public enum CourseType : byte
    {
        Open = 0,
        Weekday = 1,
        Weekend = 2
    }
}
