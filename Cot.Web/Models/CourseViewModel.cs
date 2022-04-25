using Cot.Web.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cot.Web.Models
{
    public class CourseViewModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Level { get; set; }
        public string AddedDateTime { get; set; }
        public string ModifiedDateTime { get; set; }
    }
}
