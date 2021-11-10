using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.DevTest.API.ViewModels
{
    public class BaseViewModel
    {
        public bool IsActive { get; set; }
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
