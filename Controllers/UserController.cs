using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upcity.Data;
using upcity.Data.UserRepo;
using upcity.Domain.Models;
using upcity.Domain.ModelsDTO;
using upcity.Helpers;

namespace upcity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public UserController(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;

        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                Guid userId = new Guid(token.Issuer);

                var user = _userRepository.GetUserByGuid(userId);

                return Ok(user);

            }catch(Exception e)
            {
                return Unauthorized();
            }
            
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDTO userDTO)
        {

            User user = _userRepository.CreateUser(userDTO);
            if (user != null)
            {
                return Created("created",JsonConvert.SerializeObject(new ResponseSchema(201, "Rejestracja pomyślna", new { email = user.Email })));
            }

            return BadRequest(new ResponseSchema(400, "Taki użytkownik już istnieje", null));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserDTO userDTO)
        {
            var user = _userRepository.LoginUser(userDTO);

            if (user == null) return BadRequest(new { message = "Podany email lub hasło są niepoprawne" });

            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });

            
            return Ok(JsonConvert.SerializeObject(new ResponseSchema( 200,"Logowanie pomyślne", new { jwt })));
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout() {

            Response.Cookies.Delete("jwt");

            return Ok(new { message = "Cookie removed" });
        }

    }
}
