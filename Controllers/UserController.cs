using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upcity.Data;
using upcity.Data.UserRepo;
using upcity.Domain.Models;
using upcity.Domain.ModelsDTO;

namespace upcity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return Ok(Json("siema"));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO userDTO)
        {

            User user = _userRepository.CreateUser(userDTO);
            return Created("created", user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserDTO userDTO)
        {
            var user = _userRepository.LoginUser(userDTO);

            if (user == null) return BadRequest(new { message = "Podany email lub hasło są niepoprawne" });

            return Ok();
        }
    }
}
