using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cot.Data.Core.Domain
{
    public class Student
    {
        public Guid Id { get; set; }
        [NotMapped]
        public string RegNo { get; set; }
    }
}