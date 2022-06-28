using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace upcity.Helpers
{
    public class JwtService
    {
        private readonly string secureKey = "secure key is very secured";
        public string Generate()
        {
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            return "";
        }
    }
}
