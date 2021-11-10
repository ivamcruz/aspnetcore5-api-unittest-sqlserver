using System.Collections.Generic;

namespace Portal.DevTest.Date.Model
{
    public class ProductModel: BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public List<OrderItemModel> lstOrderItems { get; set; }
    }
}
