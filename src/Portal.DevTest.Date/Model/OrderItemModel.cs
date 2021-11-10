using System;

namespace Portal.DevTest.Date.Model
{
    public class OrderItemModel : BaseModel
    {   

        public Guid OrderId { get; set; }
        public OrderModel Order { get; set; }

        public Guid ProductId { get; set; }
        public ProductModel Product { get; set; }

        public int Amount { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
