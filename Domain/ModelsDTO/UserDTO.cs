using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upcity.Domain.ModelsDTO
{
    public class UserDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public UserDTO()
        {

        }
        public UserDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
