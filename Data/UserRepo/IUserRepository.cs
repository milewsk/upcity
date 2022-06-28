using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upcity.Domain.Models;
using upcity.Domain.ModelsDTO;

namespace upcity.Data.UserRepo
{
    public interface IUserRepository
    {

        User GetUser(string email);


        User CreateUser(UserDTO user);

        User LoginUser(UserDTO user);
        
    }
}
