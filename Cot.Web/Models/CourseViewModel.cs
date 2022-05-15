using Cot.Data.Core.Domain;
using System.Collections.Generic;

namespace Cot.Web.Models
{
    public class CourseViewModel : IViewModel
    {
        public Course Course { get; set; }

        public IList<string> Syllabuses { get; set; }
        public IList<string> Classes { get; set; }
    }
}
