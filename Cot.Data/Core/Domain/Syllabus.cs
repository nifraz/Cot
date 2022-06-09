using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cot.Data.Core.Domain
{
    public class Syllabus
    {
        public Course Course { get; set; }
        public ICollection<Batch> Batches { get; set; } = new HashSet<Batch>();
    }
}
