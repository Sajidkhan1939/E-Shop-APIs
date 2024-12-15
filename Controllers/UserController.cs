using DAL.Interface.UserInterface;
using JwtImplementation.BLL.Model.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtImplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userrepo;

        public UserController(IUser userrepo)
        {
            _userrepo = userrepo;
        }
        [Authorize (Roles = "Admin")]
        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetUserall()
        {
            var allusers = await _userrepo.GetAllUser();
            if (allusers != null)
                return Ok(allusers);
            else
                return Ok("User not found");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser(string email)
        {
            var userbyemail = await _userrepo.GetUserByEmail(email);
            if (userbyemail != null)
            {
                return Ok(userbyemail);
            }
            else
                return Ok("User not found");
        }

        [Authorize]
        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUser()
        {
            var permission = User.Claims.FirstOrDefault(c => c.Type == "Permissions");
            if (permission != null && permission.Value.Contains("Read"))
            {
                var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var email = emailClaim?.Value;
                var userbyemail = await _userrepo.GetUserByEmail(email);
                if (userbyemail != null)
                    return Ok(userbyemail);
                else
                    return Ok("User not found");
            }
            if (permission != null && permission.Value.Contains("Read,Write"))
            {
                var allusers = await _userrepo.GetAllUser();
                if (allusers != null)
                    return Ok(allusers);
                else
                    return Ok("User not found");
            }
            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(User user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Invalid user data.");
            }

            var newUser = new User
            {
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password,
                RoleId = user.RoleId
            };

            var registeredUser = await _userrepo.RegisterUser(newUser);

            if (registeredUser != null)
            {
                return Ok(new { Message = "User registered successfully.", User = registeredUser });
            }

            return BadRequest("User registration failed. The user may already exist.");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var jwttoken = await _userrepo.Login(email, password);
            if (jwttoken == null)
                return Unauthorized();

            return Ok(new { token = jwttoken });
        }

        private IActionResult GetClaims()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();

            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);
                var claims = jwtToken.Claims.Select(c => new { c.Type, c.Value });

                return Ok(claims);
            }

            return BadRequest("Invalid token.");
        }
    }
}
