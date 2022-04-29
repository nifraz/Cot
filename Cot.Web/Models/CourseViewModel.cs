using Cot.Web.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cot.Web.Models
{
    public class CourseViewModel : IViewModel
    {
        public Course Course { get; set; }

        public IList<string> Syllabuses { get; set; }
        public IList<string> Classes { get; set; }
    }
}
