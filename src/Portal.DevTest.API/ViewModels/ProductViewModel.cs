using Portal.DevTest.Date.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portal.DevTest.API.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {

        [Required()]
        [StringLength(50, MinimumLength = 6)]
        public string Name { get; set; }
        
        [StringLength(50, MinimumLength = 6)]
        public string Description { get; set; }

        [Required()]
        public decimal Price { get; set; }

        //public List<OrderItemViewModel> lstOrderItems { get; set; }
    }
}
