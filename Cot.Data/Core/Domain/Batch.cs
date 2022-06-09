using System.Collections;
using System.Collections.Generic;

namespace Cot.Data.Core.Domain
{
    public class Batch
    {
        public Syllabus Syllabus { get; set; }
        public ICollection<Student> Student { get; set; } = new HashSet<Student>();
    }
}