using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upcity.Database;
using upcity.Domain.Models;
using upcity.Domain.ModelsDTO;

namespace upcity.Data.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User GetUserByGuid(Guid guid) {
            var user = (from x in _context.Users where guid == x.Id select x).FirstOrDefault();

            return user;
        }

        public User GetUserByEmail(string email)
        {
            var user = (from x in _context.Users where email == x.Email select x).FirstOrDefault();

            return user;
        }

        public User CreateUser(UserDTO userDTO)
        {
            //logic for validation 
            if (GetUserByEmail(userDTO.Email) == null)
            {
                User user = new User(userDTO.Email, BCrypt.Net.BCrypt.HashPassword(userDTO.Password));
                _context.Users.Add(user);
                _context.SaveChanges();

                user = GetUserByEmail(user.Email);

                return user;
            }

            return null;
        }

        public User LoginUser(UserDTO userDTO)
        {
            if (true)
            {
                var user = GetUserByEmail(userDTO.Email);

                if (user == null) return null;

                if (!BCrypt.Net.BCrypt.Verify(userDTO.Password, user.Password))
                {
                    return null;
                }

                return user;
            }           
        }
    }
}
