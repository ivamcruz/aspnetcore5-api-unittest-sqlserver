using Portal.DevTest.Business.Interfaces;
using Portal.DevTest.Date.Interfaces;
using Portal.DevTest.Date.Model;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Portal.DevTest.Date.Filters;

namespace Portal.DevTest.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private StringBuilder _logsErros;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logsErros = new StringBuilder();
        }

        public async Task<StringBuilder> AddNew(UserModel user)
        {
            if (UserExists(user))
                return _logsErros.AppendLine("Username or email already exists");

            user.Id = Guid.NewGuid();
            user.CreationDate = DateTime.Now;
            user.IsActive = true;

            try { await _userRepository.Add(user); }
            catch { _logsErros.Append("There was an error entering the user"); }

            return _logsErros;
        }


        public List<UserModel> Search(UsersFilter filterUser)
        {
            List<UserModel> lstUsers = new List<UserModel>();

            try
            {
                lstUsers = _userRepository.Search(a => a.IsActive.HasValue).Result.ToList();

                if (!string.IsNullOrEmpty(filterUser.UserName))
                    lstUsers = lstUsers.Where(x => x.UserName.ToLower().Contains(filterUser.UserName)).ToList();

                if (!string.IsNullOrEmpty(filterUser.DisplayName))
                    lstUsers = lstUsers.Where(x => x.DisplayName.ToLower().Contains(filterUser.DisplayName)).ToList();

                if (!string.IsNullOrEmpty(filterUser.Email))
                    lstUsers = lstUsers.Where(x => x.Email.ToLower().Contains(filterUser.Email)).ToList();

                if (filterUser.StartDate.HasValue && filterUser.EndDate.HasValue)
                    lstUsers = lstUsers.Where(x =>
                        x.CreationDate >= filterUser.StartDate && x.CreationDate <= filterUser.EndDate)
                                                .ToList();

                if (!string.IsNullOrEmpty(filterUser.Order) && !string.IsNullOrEmpty(filterUser.PropertySort))
                {
                    if (filterUser.Order.ToLower() == "asc" && filterUser.PropertySort.ToLower() == "username")
                        lstUsers = lstUsers.OrderBy(x => x.UserName).ToList();

                    else if (filterUser.Order.ToLower() == "desc" && filterUser.PropertySort.ToLower() == "username")
                        lstUsers = lstUsers.OrderByDescending(x => x.UserName).ToList();

                    else if (filterUser.Order.ToLower() == "asc" && filterUser.PropertySort.ToLower() == "displayname")
                        lstUsers = lstUsers.OrderBy(x => x.DisplayName).ToList();

                    else if (filterUser.Order.ToLower() == "desc" && filterUser.PropertySort.ToLower() == "displayname")
                        lstUsers = lstUsers.OrderByDescending(x => x.DisplayName).ToList();

                    else if (filterUser.Order.ToLower() == "asc" && filterUser.PropertySort.ToLower() == "email")
                        lstUsers = lstUsers.OrderBy(x => x.Email).ToList();

                    else if (filterUser.Order.ToLower() == "desc" && filterUser.PropertySort.ToLower() == "email")
                        lstUsers = lstUsers.OrderByDescending(x => x.Email).ToList();
                }
            }
            catch (Exception) { return lstUsers; }

            return lstUsers;
        }

        public bool UserExists(UserModel user)
        {
            List<UserModel> lstUsers = new List<UserModel>();
            try { lstUsers = _userRepository.Search(x => x.Email == user.Email || x.UserName == user.UserName).Result.ToList(); }
            catch { _logsErros.Append("An error occurred while trying to list the users"); }

            return lstUsers.Count() > 0;
        }

        public StringBuilder Login(UserModel user)
        {
            bool isValidUser = false;
            try { isValidUser = _userRepository.Search(x => x.UserName == user.UserName && x.Password == user.Password).Result.Any(); }
            catch { _logsErros.AppendLine("There was an error trying to locate the user"); }

            if (!isValidUser)
                return _logsErros.AppendLine("Username or password is invalid");

            return _logsErros;
        }
    }
}
