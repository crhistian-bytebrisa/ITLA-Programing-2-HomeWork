using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.DTOs.API
{
    public class JWTResponse
    {
        public ApplicationUserDTO User { get; set; }
        public List<string> Roles { get; set; }
        public DateTime ExpirationToken { get; set; }
        public string Token { get; set; }
    }

    public class APIJWTResponse
    {
        public ApplicationUserDTO User { get; set; }
        public List<string> Roles { get; set; }
        public APIJWTResponse(ApplicationUserDTO user, List<string> roles)
        {
            User = user;
            Roles = roles;
        }
    }
}
