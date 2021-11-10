using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.DevTest.API.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        public Guid UserId { get; set; }
        public UserViewModel User { get; set; }

        public List<OrderItemViewModel> lstOrderItems { get; set; }

    }
}
