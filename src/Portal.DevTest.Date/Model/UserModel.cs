using System.Collections.Generic;

namespace Portal.DevTest.Date.Model
{
    public class UserModel : BaseModel
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public List<OrderModel> lstOrders { get; set; }
    }
}
