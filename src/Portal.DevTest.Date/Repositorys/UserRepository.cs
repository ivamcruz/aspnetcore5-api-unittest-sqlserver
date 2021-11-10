using Portal.DevTest.Date.Context;
using Portal.DevTest.Date.Interfaces;
using Portal.DevTest.Date.Model;

namespace Portal.DevTest.Date.Repositorys
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public UserRepository(ContextSQLServer context) : base(context) { }
    }
}
