using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portal.DevTest.API.ViewModels
{
    public class UserViewModel : BaseViewModel
    {

        [Required()]
        [StringLength(30, MinimumLength = 6)]
        public string UserName { get; set; }

        [Required()]
        [StringLength(60, MinimumLength = 6)]
        public string DisplayName { get; set; }

        [Required()]
        [StringLength(100, MinimumLength = 6)]
        [EmailAddress]
        public string Email { get; set; }

        [Required()]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        public List<OrderViewModel> lstOrders { get; set; }

    }
}
