using Portal.DevTest.Date.Filters;
using Portal.DevTest.Date.Model;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DevTest.Business.Interfaces
{
    public interface IUserService
    {
        Task<StringBuilder> AddNew(UserModel user);
        StringBuilder Login(UserModel user);
        List<UserModel> Search(UsersFilter userFilter);
        bool UserExists(UserModel user);
    }
}
