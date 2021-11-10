using Portal.DevTest.Date.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.DevTest.API.ViewModels
{
    public class OrderItemViewModel : BaseViewModel
    {   

        public Guid OrderId { get; set; }
        public OrderViewModel Order { get; set; }

        public Guid ProductId { get; set; }
        public ProductViewModel Product { get; set; }

        public int Amount { get; set; }
        public decimal CurrentPrice { get; set; }

    }
}
