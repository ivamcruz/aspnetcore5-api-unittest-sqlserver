using System;
using System.Collections.Generic;

namespace Portal.DevTest.Date.Model
{
    public class OrderModel : BaseModel
    {   
        public Guid UserId { get; set; }
        public UserModel User { get; set; }

        public List<OrderItemModel> lstOrderItems { get; set; }
    }
}
