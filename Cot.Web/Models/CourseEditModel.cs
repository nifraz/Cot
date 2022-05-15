using Cot.Data.Core.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cot.Web.Models
{
    public class CourseEditModel
    {
        [Display(Name = "UID")]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(32)]
        public string Code { get; set; }
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }
        public CourseLevel Level { get; set; }
        public CourseType Type { get; set; }
        public string Notes { get; set; }
    }
}
