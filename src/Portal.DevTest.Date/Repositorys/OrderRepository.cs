using Portal.DevTest.Date.Context;
using Portal.DevTest.Date.Interfaces;
using Portal.DevTest.Date.Model;

namespace Portal.DevTest.Date.Repositorys
{
    public class OrderRepository : Repository<OrderModel>, IOrderRepository
    {
        public OrderRepository(ContextSQLServer context) : base(context) { }
    }
}
