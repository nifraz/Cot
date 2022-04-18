using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cot.Web.Core.Domain
{
    public class Course : IEntity
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }
}
