using Portal.DevTest.Date.Context;
using Portal.DevTest.Date.Interfaces;
using Portal.DevTest.Date.Model;

namespace Portal.DevTest.Date.Repositorys
{
    public class ProductRepository : Repository<ProductModel>, IProductRepository
    {
        public ProductRepository(ContextSQLServer context) : base(context) { }
    }
}
