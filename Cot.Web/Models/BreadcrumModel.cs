using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cot.Web.Models
{
    public class BreadcrumModel
    {
        public string Title { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public IDictionary<string, string> Params { get; set; }

    }
}
