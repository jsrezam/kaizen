using System;

namespace Kaizen.Controllers.Resources
{
    public class ResponseAuthenticationResourse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}