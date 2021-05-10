using System;

namespace Kaizen.Core.DTOs
{
    public class ResponseAuthenticationResourse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}