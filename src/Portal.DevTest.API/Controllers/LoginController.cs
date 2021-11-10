using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Portal.DevTest.API.Configuration;
using Portal.DevTest.API.ViewModels;
using Portal.DevTest.Business.Interfaces;
using Portal.DevTest.Date.Model;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

namespace PortalTele.DevTest.API.Controllers
{
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IOptions<Auth0Config> _config;
        public LoginController(IMapper mapper, IUserService userService, IOptions<Auth0Config> config)
        {
            _config = config;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Add(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            var logsAttemptSaveNewUser = await _userService.AddNew(_mapper.Map<UserModel>(userViewModel));

            if (!string.IsNullOrEmpty(logsAttemptSaveNewUser.ToString()))
                return BadRequest(logsAttemptSaveNewUser.ToString());

            return new OkObjectResult("Created");
        }


        [HttpPost("signin")]
        public IActionResult Signin(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid) return BadRequest("Request is invalid");

            string logsValidateUser = _userService.Login(_mapper.Map<UserModel>(userViewModel)).ToString();

            if (!string.IsNullOrEmpty(logsValidateUser.ToString()))
                return BadRequest(logsValidateUser.ToString());

            if (_config == null)
                return Ok("Bearer configUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6ImtDTXg0c");

            var client = new RestClient(_config.Value.DomainUrl);
            var request = new RestRequest(Method.POST);
            var headerContent = $"{{\"client_id\":\"{_config.Value.ClientId}\",\"client_secret\":\"{_config.Value.ClientSecret}\",\"audience\":\"{_config.Value.Audience}\",\"grant_type\":\"{_config.Value.GrantType}\"}}";
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", headerContent, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var customer = JsonSerializer.Deserialize<ResponseAuth0>(response.Content.ToString());

            return Ok($"Bearer {customer.access_token}");
        }
    }
}
