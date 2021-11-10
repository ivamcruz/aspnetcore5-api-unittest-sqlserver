using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.DevTest.Date.Interfaces;
using Portal.DevTest.Date.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortalTele.DevTest.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet("users")]
        [Authorize("read:messages")]
        public ActionResult<List<UserModel>> GetAll()
        {
            List<UserModel> lstUsers;
            try { lstUsers = _userRepository.Search(x => x.IsActive.HasValue).Result.ToList(); } catch (Exception) { return BadRequest("Get all is fail"); }
            return Ok(lstUsers);
        }
    }

}
