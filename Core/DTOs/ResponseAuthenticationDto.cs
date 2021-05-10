using System;

namespace Kaizen.Core.DTOs
{
    public class ResponseAuthenticationDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}