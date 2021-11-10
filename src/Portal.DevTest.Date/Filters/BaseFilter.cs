using System;

namespace Portal.DevTest.Date.Filters
{
    public class BaseFilter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Order { get; set; }
        public string PropertySort { get; set; }
    }
  
}
