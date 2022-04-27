using Cot.Web.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cot.Web.Models
{
    public class CourseCreateModel
    {
        [Required]
        [StringLength(32)]
        public string Code { get; set; }
        public string Title { get; set; }
        public CourseLevel Level { get; set; }
        public CourseType Type { get; set; }
        public string Notes { get; set; }
    }
}
