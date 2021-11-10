using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DevTest.Date.Filters
{
    public class ProductsFilter : BaseFilter
    {   
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
    }

    
}
