using Cot.Data.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Cot.Web.Models
{
    public class CourseCreateModel
    {
        [Required]
        [MaxLength(32)]
        [Remote(action: "ValidateCourseCode", controller: "Courses")]    //calls the function using ajax to validate
        public string Code { get; set; }
        [Required]
        [MaxLength(256)]
        [Remote(action: "ValidateCourseTitle", controller: "Courses")]
        public string Title { get; set; }
        public CourseLevel Level { get; set; }
        public CourseType Type { get; set; }
        public string Notes { get; set; }
    }
}
