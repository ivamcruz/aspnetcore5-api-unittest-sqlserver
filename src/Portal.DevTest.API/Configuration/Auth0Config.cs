using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.DevTest.API.Configuration
{
    public class Auth0Config
    {
        public string Domain { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Audience { get; set; }
        public string GrantType { get; set; }
        public string DomainUrl { get; set; }
    }

    public class ResponseAuth0
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
    }
}
