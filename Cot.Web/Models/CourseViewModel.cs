using Cot.Web.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cot.Web.Models
{
    public class CourseViewModel : IViewModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Level { get; set; }
        public string Type { get; set; }
        public string AddedDate { get; set; }
        public string ModifiedDate { get; set; }
        public string Notes { get; set; }
    }
}
