using Portal.DevTest.Date.Filters;
using Portal.DevTest.Date.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DevTest.Business.Interfaces
{
    public interface IOrderService
    {
        StringBuilder AddNew(OrderModel product);
        List<OrderModel> Search(OrdersFilter orderFilter);
    }
}
